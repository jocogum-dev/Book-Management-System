using System.Text.Json;
using BMS.Data;
using BMS.Models;
using Microsoft.EntityFrameworkCore;

namespace BMS.Scanner;

public static class ScannerProgram
{
    public static void Run(string[] args)
    {
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
            List<BookFile> books = ScanForPdfs(rootFolder);
            Console.WriteLine($"--- Found {books.Count} PDF files. --");

            // save data to db
            SaveToSQLite(books);

        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occured: {ex.Message}");
        }
    }

    // -- functions
    private static List<BookFile> ScanForPdfs(string rootFolder)
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
                Console.WriteLine($"Skipped: {file}");
            }
        }

        return results;
    }

    private static void PrintHelp()
    {
        Console.WriteLine("""
        BMS.Scanner - PDF file scanner

        Usage:
            dotnet run -- <folder>

        Example:
            dotnet run -- "/home/user/Books"
        """);
    }

    private static void SaveToSQLite(List<BookFile> books)
    {
        var repoRoot = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;
        var dataFolder = Path.Combine(repoRoot, "BMS.Data");
        if (!Directory.Exists(dataFolder))
        {
            Directory.CreateDirectory(dataFolder);
        }
        var dbPath = Path.Combine(dataFolder, "books.db");
        var connectionString = $"Data Source={dbPath}";

        var options = new DbContextOptionsBuilder<BookDbContext>()
            .UseSqlite(connectionString)
            .Options;

        using (var db = new BookDbContext(options))
        {
            db.Database.EnsureCreated();

            int addedCount = 0;
            var existingPaths = new HashSet<string>(db.Books.Select(b => b.FullPath));
            foreach (var book in books)
            {
                try
                {
                    if (!existingPaths.Contains(book.FullPath))
                    {
                        db.Books.Add(book);
                        existingPaths.Add(book.FullPath);
                        addedCount++;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Skipped book {book.FileName}: {ex.Message}");
                }
            }

            try
            {
                db.SaveChanges();
                Console.WriteLine($"Added {addedCount} new books to the database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to DB: {ex.Message}");
            }
        }
    }
}

