using BMS.Data;
using BMS.Models;
using BMS.Services;
using BMS.Web.ViewModels.Dashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BMS.Web.Pages.Dashboard
{
    [Authorize(Roles = "Admin")]
    public class DashboardModel : PageModel
    {
        private readonly BookDbContext _db;
        private readonly ThumbnailService _thumbnailService;
        public DashboardViewModel Summary { get; set; } = new DashboardViewModel();
        public List<BookFile> LatestAdded { get; set; } = new List<BookFile>();
        public List<BookWithThumbnailViewModel> BooksWithThumbnail { get; set; } = new List<BookWithThumbnailViewModel>();
        public DashboardModel(BookDbContext db, ThumbnailService thumbnailService)
        {
            _db = db;
            _thumbnailService = thumbnailService;
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
            foreach (var book in LatestAdded)
            {
                var thumbnailPath = Path.Combine("wwwroot/thumbnails", $"{book.Id}.jpg");
                if (!System.IO.File.Exists(thumbnailPath))
                {
                    _thumbnailService.GetOrGenerateThumbnail(book.DirectoryPath, book.FileName, book.Id);
                }
                BooksWithThumbnail.Add(new BookWithThumbnailViewModel
                {
                    Id = book.Id,
                    Title = book.FileName,
                    AddedAt = book.AddedAt,
                    ThumbnailPath = $"/thumbnails/{book.Id}.jpg"
                });
            }
        }
    }
}
