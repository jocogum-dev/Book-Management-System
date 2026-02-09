using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BMS.Web.Pages.Account
{
    public class AssignRoleModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AssignRoleModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [BindProperty]
        [Required]
        public string? SelectedUserId { get; set; }

        [BindProperty]
        [Required]
        public string? Role { get; set; }

        public string? Message { get; set; }
        public List<IdentityUser> Users { get; set; } = new List<IdentityUser>();
        private readonly string[] allowedRoles = new[] { "Admin", "User" };
        public async Task OnGetAsync()
        {
            // load users
            var currentUser = User.Identity?.Name;
            if (currentUser != null)
            {
                Users = _userManager.Users.Where(u => u.Email != currentUser).ToList();
            }
            else
            {
                Users = new List<IdentityUser>();
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(SelectedUserId) || string.IsNullOrEmpty(Role))
        {
            Message = "User and Role are required.";
            await OnGetAsync(); // reload users for the dropdown
            return Page();
        }
            // check role
            if (!allowedRoles.Contains(Role))
            {
                Message = "Role must be either 'Admin' or 'User'.";
                return Page();
            }
            // find user
            var user = await _userManager.FindByIdAsync(SelectedUserId);
            if (user == null)
            {
                Message = "User not found.";
                await OnGetAsync();
                return Page();
            }
            // Ensure the role exists
            if (!await _roleManager.RoleExistsAsync(Role))
            {
                await _roleManager.CreateAsync(new IdentityRole(Role));
            }
            // Remove user from other role first
            foreach (var r in allowedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, r))
                    await _userManager.RemoveFromRoleAsync(user, r);
            }
            // Assign the new role
            var result = await _userManager.AddToRoleAsync(user, Role);
            if (result.Succeeded)
            {
                Message = $"{user.Email} is now assigned to role {Role}.";
            }
            else
            {
                Message = string.Join(", ", result.Errors.Select(e => e.Description));
            }
            await OnGetAsync(); // reload users for dropdown
            return RedirectToPage("/Dashboard/Index");
        }
    }
}
