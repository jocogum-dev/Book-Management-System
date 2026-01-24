
// check for args
if (args.Length == 0 || args[0] == "--help")
{
    PrintHelp();
    return;
}
// check folder
string rootFolder = Path.GetFullPath(args[0]);

if (!Directory.Exists(rootFolder))
{
    Console.WriteLine($"Folder not found: {rootFolder}");
    return;
}
Console.WriteLine($"Scanning for PDF files:\n{rootFolder}");

// scan
try
{
    ScanForPdfs(rootFolder);
}
catch (Exception ex)
{
    Console.WriteLine($"An error occured: {ex.Message}");
}


static void ScanForPdfs(string rootFolder)
{
    var pdfFiles = Directory.EnumerateFiles(
    rootFolder,
    "*.pdf",
    SearchOption.AllDirectories
);
    int count = 0;

    foreach (var file in pdfFiles)
    {
        var info = new FileInfo(file);

        Console.WriteLine($"Book: {info.Name}");
        Console.WriteLine($"Path: {info.FullName}");
        Console.WriteLine($"Size: {info.Length / 1024} KB");
        Console.WriteLine($"Modified: {info.LastWriteTime}");
        Console.WriteLine();

        count++;
    }
    Console.WriteLine($"Done.Found {count} PDF files");
}

void PrintHelp()
{
    Console.WriteLine("""
    BMS.Scanner - PDF file scanner

    Usage:
      dotnet run -- <folder>

    Example:
      dotnet run -- "/home/user/Books"
    """);
}