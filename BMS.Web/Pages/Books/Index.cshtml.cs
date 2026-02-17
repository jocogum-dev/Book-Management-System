using BMS.Data;
using BMS.Models;
using BMS.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BMS.Web.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly BookDbContext _db;
        public IndexModel(BookDbContext db)
        {
            _db = db;
        }
        public PaginatedList<BookFile> Books { get; set; } = new PaginatedList<BookFile>([], 0, 1, 10);
        public async Task OnGetAsync(string? sort, string? dir, string? search, int? pageIndex)
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
            int pageSize = 50;
            Books = await PaginatedList<BookFile>.CreateAsync(myQuery, pageIndex ?? 1, pageSize);
        }
    }
}
