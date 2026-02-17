using BMS.Data;
using BMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BMS.Web.Pages.Books
{
    public class ReadModel : PageModel
    {
        private readonly BookDbContext _db;
        public BookFile? Books { get; set; } = default!;
        public ReadModel(BookDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
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
            return RedirectToPage("/Account/Login");
        }
    }
}
