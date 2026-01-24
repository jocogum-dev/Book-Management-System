using System.Text.Json;
using BMS.Models;
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

var outputFile = args.Length > 1 ? Path.GetFullPath(args[1]) : Path.Combine(Directory.GetCurrentDirectory(), "books.json");
var outputDir = Path.GetDirectoryName(outputFile)!;
if (!Directory.Exists(outputDir))
{
    Directory.CreateDirectory(outputDir);
    Console.WriteLine($"Created output folder: {outputDir}");
}
Console.WriteLine($"JSON output will be saved to: {outputFile}\n");

// scan
try
{
    List<BookFile> books = ScanForPdfs(rootFolder);
    // foreach (var book in books)
    // {
    //     Console.WriteLine($"Book: {book.FileName}");
    //     Console.WriteLine($"Path: {book.FullPath}");
    //     Console.WriteLine($"Size: {book.SizeBytes / 1024} KB");
    //     Console.WriteLine();
    // }
    // serialize to json
    var jsonOptions = new JsonSerializerOptions
    {
        WriteIndented = true
    };
    var json = JsonSerializer.Serialize(books, jsonOptions);
    File.WriteAllText(outputFile, json);

    Console.WriteLine($"--- Found {books.Count} PDF files. --");
}
catch (Exception ex)
{
    Console.WriteLine($"An error occured: {ex.Message}");
}

// -- functions
List<BookFile> ScanForPdfs(string rootFolder)
{
    var results = new List<BookFile>();

    foreach (var file in Directory.EnumerateFiles(rootFolder, "*.pdf", SearchOption.AllDirectories))
    {
        try
        {
            var info = new FileInfo(file);
            results.Add(new BookFile
            {
                Id = Guid.NewGuid(),
                FileName = info.Name,
                FullPath = info.FullName,
                DirectoryPath = info.DirectoryName!,
                SizeBytes = info.Length,
                LastModified = info.LastWriteTimeUtc,
                AddedAt = DateTime.UtcNow
            });
        }
        catch
        {
            // Skip files that can't be accessed
            Console.WriteLine($"Skipped: {file}");
        }
    }

    return results;
}

void PrintHelp()
{
    Console.WriteLine("""
    BMS.Scanner - PDF file scanner

    Usage:
        dotnet run -- <folder> [output-file]

    Example:
        dotnet run -- "/home/user/Books" "Data/mybooks.json"

    Notes:
        - If output file is not specified, defaults to books.json in current directory.
        - The output folder will be created automatically if it does not exist.
    """);
}