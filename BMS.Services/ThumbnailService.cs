using System.Diagnostics;

namespace BMS.Services;

public class ThumbnailService
{
    public void GenerateThumbnail(string directoryPath,string pdfPath, string outputPath)
    {
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
        process.StartInfo.FileName = "/usr/bin/convert";
        process.StartInfo.ArgumentList.Add("-density");
        process.StartInfo.ArgumentList.Add("150");
        process.StartInfo.ArgumentList.Add($"{directoryPath}/{pdfPath}[0]");
        process.StartInfo.ArgumentList.Add("-quality");
        process.StartInfo.ArgumentList.Add("90");
        process.StartInfo.ArgumentList.Add("-resize");
        process.StartInfo.ArgumentList.Add("300x");
        process.StartInfo.ArgumentList.Add(outputPath);
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.UseShellExecute = false;

        process.Start();
        process.WaitForExit();

        if (process.ExitCode != 0)
        {
            var error = process.StandardError.ReadToEnd();
            throw new Exception($"Generate thumbnail faile: {error}");
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
