# Video Frame to PDF (Windows Forms)

A Windows Forms utility that extracts one frame per second from an MP4 video, previews the first extracted frame, and builds a multi-page PDF (one image per page). Temporary images are deleted automatically after the PDF is created.

## Requirements

- Windows with .NET 8.0 SDK or later installed.
- FFmpeg binaries available on the PATH (needed by `AForge.Video.FFMPEG`).

## Setup

```bash
# Restore NuGet packages
# (run from the `VideoFramePdfExtractor` folder)
dotnet restore

# Build and run
# (requires Windows because the project targets Windows-specific APIs)
dotnet run
```

If you prefer the NuGet Package Manager instead of the .NET CLI, add the dependencies via:

- `AForge.Video.FFMPEG` (2.2.5)
- `PdfSharp` (1.50.5147)

## How it works

1. Choose an MP4 video file.
2. The app extracts one frame per second to a temporary directory using `AForge.Video.FFMPEG`.
3. The first extracted frame is displayed in the preview box.
4. `PdfSharp` writes each extracted image to its own PDF page.
5. Temporary files are cleaned up automatically after the PDF is saved.
