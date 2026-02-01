using BMS.Data;
using BMS.Models;
using BMS.Web.ViewModels.Dashboard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BMS.Web.Pages.Dashboard
{
    public class DashboardModel : PageModel
    {
        private readonly BookDbContext _db;
        public DashboardViewModel Summary { get; set; } = new DashboardViewModel();
        public List<BookFile> LatestAdded { get; set; } = new List<BookFile>();
        public DashboardModel(BookDbContext db)
        {
            _db = db;
        }
        public async Task OnGetAsync()
        {
            Summary = new DashboardViewModel
            {
                TotalBooks = await _db.Books.CountAsync(),
                TotalSizeBytes = await _db.Books.SumAsync(b => b.SizeBytes),
                UniqueFolders = await _db.Books.Select(b => b.DirectoryPath).Distinct().CountAsync()
            };
            LatestAdded = await _db.Books.OrderByDescending(b => b.AddedAt).Take(10).ToListAsync();
        }
    }
}
