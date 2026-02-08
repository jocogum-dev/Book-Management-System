using BMS.Data;
using BMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BMS.Web.Pages.Books
{
    public class DeleteModel : PageModel
    {
        private readonly BookDbContext _db;
        [BindProperty]
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
        public async Task<IActionResult> OnPostAsync()
        {
            if (Book == null)
            {
                return NotFound();
            }
            var book = await _db.Books.FindAsync(Book.Id);
            if (book == null)
            {
                return NotFound();
            }
            _db.Books.Remove(book);
            await _db.SaveChangesAsync();

            return RedirectToPage("/Books/Index");
        }
    }
}
