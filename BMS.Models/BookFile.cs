namespace BMS.Models;

public class BookFile
{
    public Guid Id { get; set; }
    public string FileName { get; set; } = default!;
    public string FullPath { get; set; } = default!;
    public string DirectoryPath { get; set; } = default!;
    public long SizeBytes { get; set; }
    public DateTime LastModified { get; set; }
    public DateTime AddedAt { get; set; }
}
