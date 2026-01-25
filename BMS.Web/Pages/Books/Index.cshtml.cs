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
        public async Task OnGetAsync()
        {
            Books = await _db.Books.OrderBy(b => b.FileName).ToListAsync();
        }
    }
}
