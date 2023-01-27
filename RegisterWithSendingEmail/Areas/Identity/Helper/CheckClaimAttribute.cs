using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Identity_Sample.Areas.Identity.Helper;

public class ClaimRequirementAttribute : TypeFilterAttribute
{
    public ClaimRequirementAttribute(string claimType, string claimValue) : base(typeof(ClaimRequirementFilter))
    {
        Arguments = new object[] { new Claim(claimType, claimValue) };
    }
}

public class ClaimRequirementFilter : IAuthorizationFilter
{
    readonly Claim _claim;

    public ClaimRequirementFilter(Claim claim)
    {
        _claim = claim;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && c.Value == _claim.Value);
        if (!hasClaim)
        {
            context.Result = new ForbidResult();
        }
    }
}