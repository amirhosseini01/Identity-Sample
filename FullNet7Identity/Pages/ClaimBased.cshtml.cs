using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FullNet7Identity.Pages
{
    [Authorize(Policy = "Founders")]
    //[Authorize(Policy = "Second")]
    public class ClaimBasedModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}