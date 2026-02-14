using System;

namespace BMS.Web.ViewModels.Dashboard;

public class BookWithThumbnailViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
    public string FilePath { get; set; } = "";
    public DateTime AddedAt { get; set; }
    public string ThumbnailPath { get; set; } = "";
}
