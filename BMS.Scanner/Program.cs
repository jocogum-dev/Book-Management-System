// check folder
string rootFolder = "/home/user/Documents";

if (!Directory.Exists(rootFolder))
{
    Console.WriteLine($"Folder not found: {rootFolder}");
    return;
}
Console.WriteLine($"Scanning for PDF files:\n{rootFolder}");

// scan
var pdfFiles = Directory.EnumerateFiles(
    rootFolder,
    "*.pdf",
    SearchOption.AllDirectories
);
int count = 0;

foreach (var file in pdfFiles)
{
    var info = new FileInfo(file);

    Console.WriteLine($"📄 {info.Name}");
    Console.WriteLine($"   Path: {info.FullName}");
    Console.WriteLine($"   Size: {info.Length / 1024} KB");
    Console.WriteLine($"   Modified: {info.LastWriteTime}");
    Console.WriteLine();

    count++;
}
Console.WriteLine($"Done.Found {count} PDF files");