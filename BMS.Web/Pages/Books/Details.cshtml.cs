using BMS.Data;
using BMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BMS.Web.Pages.Books
{
    public class DetailsModel : PageModel
    {
        private readonly BookDbContext _db;
        public BookFile? Books { get; set; } = default!;
        public DetailsModel(BookDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> OnGetAsync([FromQuery] Guid id)
        {
            Books = await _db.Books.FindAsync(id);
            if (Books == null || !System.IO.File.Exists(Books.FullPath))
            {
                return NotFound();
            }
            return PhysicalFile(
                Books.FullPath,
                "application/pdf"
            );
        }
    }
}
