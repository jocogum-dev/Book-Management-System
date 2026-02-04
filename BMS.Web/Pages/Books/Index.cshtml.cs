using BMS.Data;
using BMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BMS.Web.Pages.Books
{
    public class BooksModel : PageModel
    {
        private readonly BookDbContext _db;
        public BooksModel(BookDbContext db)
        {
            _db = db;
        }
        public List<BookFile> Books { get; set; } = new List<BookFile>();
        public async Task OnGetAsync(string? sort, string? dir)
        {
            if (sort == "title")
            {
                if (dir == "desc")
                {
                    Books = await _db.Books.OrderByDescending(b => b.FileName).ToListAsync();
                }
                else
                {
                    Books = await _db.Books.OrderBy(b => b.FileName).ToListAsync();
                }
            }
            else
            {
                if (dir == "desc")
                {
                    Books = await _db.Books.OrderByDescending(b => b.LastModified).ToListAsync();
                }
                else
                {
                    Books = await _db.Books.OrderBy(b => b.LastModified).ToListAsync();
                }
            }
        }
    }
}
