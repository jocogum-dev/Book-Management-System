using BMS.Data;
using BMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BMS.Web.Pages.Books
{
    public class DeleteModel : PageModel
    {
        private readonly BookDbContext _db;
        public BookFile? Book { get; set; } = new BookFile();
        public DeleteModel(BookDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Book = await _db.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (Book == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
