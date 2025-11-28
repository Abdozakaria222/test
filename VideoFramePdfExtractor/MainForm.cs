using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video.FFMPEG;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace VideoFramePdfExtractor
{
    public partial class MainForm : Form
    {
        private string? _tempDirectory;

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnBrowseVideo_Click(object sender, EventArgs e)
        {
            using OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "MP4 video|*.mp4",
                Title = "Select an MP4 video",
                CheckFileExists = true
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtVideoPath.Text = dialog.FileName;
                lblStatus.Text = "Video selected. Choose where to save the PDF.";
                SuggestPdfLocation(dialog.FileName);
            }
        }

        private void btnBrowsePdf_Click(object sender, EventArgs e)
        {
            using SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "PDF files|*.pdf",
                Title = "Save extracted frames as PDF",
                FileName = SuggestPdfFileName()
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtPdfPath.Text = dialog.FileName;
            }
        }

        private async void btnExtract_Click(object sender, EventArgs e)
        {
            string videoPath = txtVideoPath.Text.Trim();
            string pdfPath = txtPdfPath.Text.Trim();

            if (!File.Exists(videoPath))
            {
                MessageBox.Show("Please choose a valid MP4 file before extracting.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(pdfPath))
            {
                btnBrowsePdf_Click(sender, e);
                pdfPath = txtPdfPath.Text.Trim();
            }

            if (string.IsNullOrWhiteSpace(pdfPath))
            {
                MessageBox.Show("Please provide a PDF save location.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ToggleUi(false);
            lblStatus.Text = "Extracting frames...";

            List<string> extractedImages = new List<string>();
            _tempDirectory = Path.Combine(Path.GetTempPath(), "VideoFramePdfExtractor", Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(_tempDirectory);

            try
            {
                // Extract frames on a background thread to keep the UI responsive.
                await Task.Run(() => ExtractFrames(videoPath, _tempDirectory!, extractedImages));

                if (extractedImages.Count == 0)
                {
                    lblStatus.Text = "No frames were extracted from the video.";
                    MessageBox.Show("Unable to extract any frames. Please check the video file.", "No Frames", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                lblStatus.Text = "Building PDF...";
                await Task.Run(() => CreatePdf(extractedImages, pdfPath));

                ShowPreview(extractedImages.First());
                lblStatus.Text = "Completed successfully.";
                MessageBox.Show("PDF created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                lblStatus.Text = "Failed to complete the operation.";
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CleanupTempFiles(extractedImages);
                ToggleUi(true);
            }
        }

        /// <summary>
        /// Extracts one frame per second using the AForge FFMPEG reader and saves the images in a temporary directory.
        /// </summary>
        private void ExtractFrames(string videoPath, string outputDirectory, List<string> extractedImages)
        {
            using VideoFileReader reader = new VideoFileReader();
            reader.Open(videoPath);

            // Calculate how many frames represent one second of video so we can sample at 1 FPS.
            double framesPerSecond = reader.FrameRate.ToDouble();
            int frameInterval = Math.Max(1, (int)Math.Round(framesPerSecond));

            long totalFrames = reader.FrameCount;

            for (long frameIndex = 0; frameIndex < totalFrames; frameIndex++)
            {
                using Bitmap? frame = reader.ReadVideoFrame();
                if (frame is null)
                {
                    break;
                }

                // Save every Nth frame where N equals the frame rate (one frame each second).
                if (frameIndex % frameInterval == 0)
                {
                    string fileName = Path.Combine(outputDirectory, $"frame_{extractedImages.Count:D5}.png");
                    frame.Save(fileName, ImageFormat.Png);
                    extractedImages.Add(fileName);
                }
            }
        }

        /// <summary>
        /// Builds a PDF where each page contains a single extracted image.
        /// </summary>
        private void CreatePdf(IEnumerable<string> imagePaths, string pdfPath)
        {
            using PdfDocument document = new PdfDocument();

            foreach (string imagePath in imagePaths)
            {
                using XImage image = XImage.FromFile(imagePath);
                PdfPage page = document.AddPage();

                // Match the page size to the image dimensions to avoid scaling issues.
                page.Width = image.PointWidth;
                page.Height = image.PointHeight;

                using XGraphics gfx = XGraphics.FromPdfPage(page);
                gfx.DrawImage(image, 0, 0, image.PointWidth, image.PointHeight);
            }

            Directory.CreateDirectory(Path.GetDirectoryName(pdfPath)!);
            document.Save(pdfPath);
        }

        private void ShowPreview(string imagePath)
        {
            picPreview.Image?.Dispose();
            picPreview.Image = new Bitmap(imagePath);
        }

        private void CleanupTempFiles(IEnumerable<string> imagePaths)
        {
            foreach (string imagePath in imagePaths)
            {
                try
                {
                    File.Delete(imagePath);
                }
                catch
                {
                    // Ignore cleanup errors so they do not block user feedback.
                }
            }

            if (!string.IsNullOrWhiteSpace(_tempDirectory) && Directory.Exists(_tempDirectory))
            {
                try
                {
                    Directory.Delete(_tempDirectory, true);
                }
                catch
                {
                    // Ignore cleanup errors.
                }
            }
        }

        private void ToggleUi(bool enabled)
        {
            btnExtract.Enabled = enabled;
            btnBrowseVideo.Enabled = enabled;
            btnBrowsePdf.Enabled = enabled;
        }

        private void SuggestPdfLocation(string videoPath)
        {
            string suggested = Path.ChangeExtension(videoPath, ".pdf");
            txtPdfPath.Text = suggested;
        }

        private string SuggestPdfFileName()
        {
            if (!string.IsNullOrWhiteSpace(txtVideoPath.Text))
            {
                string fileName = Path.GetFileNameWithoutExtension(txtVideoPath.Text);
                return string.IsNullOrEmpty(fileName) ? "frames.pdf" : $"{fileName}.pdf";
            }

            return "frames.pdf";
        }
    }
}
