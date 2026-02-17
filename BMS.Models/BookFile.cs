namespace BMS.Models;

public class BookFile
{
    public Guid Id { get; set; }
    public string FileName { get; set; } = String.Empty;
    public string FullPath { get; set; } = String.Empty;
    public string DirectoryPath { get; set; } = String.Empty;
    public long SizeBytes { get; set; }
    public DateTime LastModified { get; set; }
    public DateTime AddedAt { get; set; }
}
