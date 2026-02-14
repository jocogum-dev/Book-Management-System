using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BMS.Web.Pages.Error
{
    public class ErrorModel : PageModel
    {
        public string? ErrorMessage { get; set; }
        public void OnGet(string? message = null)
        {
            ErrorMessage = message ?? "An error occured.";
        }
    }
}
