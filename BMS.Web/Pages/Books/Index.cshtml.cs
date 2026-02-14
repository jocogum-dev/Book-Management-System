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
        public async Task OnGetAsync(string? sort, string? dir, string? search)
        {
            var myQuery = _db.Books.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                myQuery = myQuery.Where(b => b.FileName.ToLower().Contains(search.ToLower()));
            }

            if (sort == "title")
            {
                if (dir == "desc")
                {
                    myQuery = myQuery.OrderByDescending(b => b.FileName);
                }
                else
                {
                    myQuery = myQuery.OrderBy(b => b.FileName);
                }
            }
            else
            {
                if (dir == "desc")
                {
                    myQuery = myQuery.OrderByDescending(b => b.LastModified);
                }
                else
                {
                    myQuery = myQuery.OrderBy(b => b.LastModified);
                }
            }

            Books = await myQuery.ToListAsync();
        }
    }
}
