using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BMS.Web.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public RegisterModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [BindProperty]
        [Required]
        [EmailAddress]
        public string? RegisterEmail { get; set; }
        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string? RegisterPassword { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) {
                return Page();
            }
            var user = new IdentityUser
            {
                UserName = RegisterEmail!,
                Email = RegisterEmail!
            };
            var result = await _userManager.CreateAsync(user, RegisterPassword!);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return RedirectToPage("/Dashboard/Index");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return Page();
        }
    }
}
