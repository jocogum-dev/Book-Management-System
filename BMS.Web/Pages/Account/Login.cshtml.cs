using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BMS.Web.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
        [BindProperty]
        [Required]
        [EmailAddress]
        public string? LogInEmail { get; set; }
        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string? LogInPassword { get; set; }
        public void OnGet(){}
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var result = await _signInManager.PasswordSignInAsync(
                LogInEmail!,
                LogInPassword!,
                isPersistent: false,
                lockoutOnFailure: false
            );
            ModelState.AddModelError("", "Invalid login attempt");
            return RedirectToPage("/Books/Index");
        }
    }
}
