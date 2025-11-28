namespace VideoFramePdfExtractor
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            txtVideoPath = new TextBox();
            btnBrowseVideo = new Button();
            btnExtract = new Button();
            picPreview = new PictureBox();
            lblStatus = new Label();
            label2 = new Label();
            txtPdfPath = new TextBox();
            btnBrowsePdf = new Button();
            ((System.ComponentModel.ISupportInitialize)picPreview).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 18);
            label1.Name = "label1";
            label1.Size = new Size(107, 15);
            label1.TabIndex = 0;
            label1.Text = "MP4 video location";
            // 
            // txtVideoPath
            // 
            txtVideoPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtVideoPath.Location = new Point(16, 36);
            txtVideoPath.Name = "txtVideoPath";
            txtVideoPath.PlaceholderText = "Select an MP4 file...";
            txtVideoPath.Size = new Size(470, 23);
            txtVideoPath.TabIndex = 1;
            // 
            // btnBrowseVideo
            // 
            btnBrowseVideo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBrowseVideo.Location = new Point(492, 35);
            btnBrowseVideo.Name = "btnBrowseVideo";
            btnBrowseVideo.Size = new Size(107, 25);
            btnBrowseVideo.TabIndex = 2;
            btnBrowseVideo.Text = "Browse...";
            btnBrowseVideo.UseVisualStyleBackColor = true;
            btnBrowseVideo.Click += btnBrowseVideo_Click;
            // 
            // btnExtract
            // 
            btnExtract.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExtract.Location = new Point(492, 120);
            btnExtract.Name = "btnExtract";
            btnExtract.Size = new Size(107, 48);
            btnExtract.TabIndex = 5;
            btnExtract.Text = "Extract && Create PDF";
            btnExtract.UseVisualStyleBackColor = true;
            btnExtract.Click += btnExtract_Click;
            // 
            // picPreview
            // 
            picPreview.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            picPreview.BackColor = SystemColors.ControlLight;
            picPreview.BorderStyle = BorderStyle.FixedSingle;
            picPreview.Location = new Point(16, 186);
            picPreview.Name = "picPreview";
            picPreview.Size = new Size(583, 277);
            picPreview.SizeMode = PictureBoxSizeMode.Zoom;
            picPreview.TabIndex = 6;
            picPreview.TabStop = false;
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblStatus.Location = new Point(16, 476);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(583, 19);
            lblStatus.TabIndex = 7;
            lblStatus.Text = "Select an MP4 file to begin.";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 74);
            label2.Name = "label2";
            label2.Size = new Size(102, 15);
            label2.TabIndex = 8;
            label2.Text = "PDF save location";
            // 
            // txtPdfPath
            // 
            txtPdfPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPdfPath.Location = new Point(16, 92);
            txtPdfPath.Name = "txtPdfPath";
            txtPdfPath.PlaceholderText = "Choose where to save the PDF...";
            txtPdfPath.Size = new Size(470, 23);
            txtPdfPath.TabIndex = 3;
            // 
            // btnBrowsePdf
            // 
            btnBrowsePdf.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBrowsePdf.Location = new Point(492, 91);
            btnBrowsePdf.Name = "btnBrowsePdf";
            btnBrowsePdf.Size = new Size(107, 25);
            btnBrowsePdf.TabIndex = 4;
            btnBrowsePdf.Text = "Browse...";
            btnBrowsePdf.UseVisualStyleBackColor = true;
            btnBrowsePdf.Click += btnBrowsePdf_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(615, 504);
            Controls.Add(btnBrowsePdf);
            Controls.Add(txtPdfPath);
            Controls.Add(label2);
            Controls.Add(lblStatus);
            Controls.Add(picPreview);
            Controls.Add(btnExtract);
            Controls.Add(btnBrowseVideo);
            Controls.Add(txtVideoPath);
            Controls.Add(label1);
            MinimumSize = new Size(631, 543);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Video Frame to PDF";
            ((System.ComponentModel.ISupportInitialize)picPreview).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtVideoPath;
        private Button btnBrowseVideo;
        private Button btnExtract;
        private PictureBox picPreview;
        private Label lblStatus;
        private Label label2;
        private TextBox txtPdfPath;
        private Button btnBrowsePdf;
    }
}
