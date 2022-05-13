using Identity_Sample.Areas.Identity.Helper;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Identity_Sample.Pages;

[ClaimRequirementAttribute(claimType: "test", claimValue: "2")]
public class PrivacyModel : PageModel
{
    private readonly ILogger<PrivacyModel> _logger;

    public PrivacyModel(ILogger<PrivacyModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {


    }
}

