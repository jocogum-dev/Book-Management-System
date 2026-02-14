using System.Diagnostics;

namespace BMS.Services;

public class ThumbnailService
{
    public void GenerateThumbnail(string directoryPath,string pdfPath, string outputPath)
    {
        var fullPdfPath = Path.Combine(directoryPath, pdfPath);
        var directory = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory!);
        }
        if (File.Exists(outputPath))
        {
            return;
        }
        var process = new Process();
        process.StartInfo.FileName = "/usr/bin/gs";
        process.StartInfo.ArgumentList.Add("-sDEVICE=jpeg");
        process.StartInfo.ArgumentList.Add("-dFirstPage=1");
        process.StartInfo.ArgumentList.Add("-dLastPage=1");
        process.StartInfo.ArgumentList.Add("-dJPEGQ=85");
        process.StartInfo.ArgumentList.Add("-r25");
        process.StartInfo.ArgumentList.Add($"-sOutputFile={outputPath}");
        process.StartInfo.ArgumentList.Add("-dNOPAUSE");
        process.StartInfo.ArgumentList.Add("-dBATCH");
        process.StartInfo.ArgumentList.Add(fullPdfPath);
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.UseShellExecute = false;

        process.Start();
        process.WaitForExit();

        if (process.ExitCode != 0)
        {
            var error = process.StandardError.ReadToEnd();
            throw new Exception($"Generate thumbnail failed: {error}");
        }
    }
    public string GetOrGenerateThumbnail(string directoryPath, string pdfPath, Guid id)
    {
        var thumbnailPath = Path.Combine("wwwroot/thumbnails", $"{id}.jpg");
        if (!File.Exists(thumbnailPath))
        {
            GenerateThumbnail(directoryPath, pdfPath, thumbnailPath);
        }
        return $"/thumbnails/{id}.jpg";
    }
}
