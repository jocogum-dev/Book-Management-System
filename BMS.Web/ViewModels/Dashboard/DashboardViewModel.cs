using System;

namespace BMS.Web.ViewModels.Dashboard;

public class DashboardViewModel
{
    public int TotalBooks { get; set; }
    public long TotalSizeBytes { get; set; }
    public DateTime? LastAdded { get; set; }
    public DateTime? LastModified { get; set; }
    public int UniqueFolders { get; set; }
    public double TotalSizeGB => Math.Round(TotalSizeBytes / 1024d / 1024d / 1024d, 2);
}
