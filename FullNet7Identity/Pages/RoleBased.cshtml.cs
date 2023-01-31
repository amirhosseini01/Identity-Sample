using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FullNet7Identity.Pages
{
    [Authorize(Roles = "myAdminTestRole")]
    public class RoleBasedModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
