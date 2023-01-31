using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FullNet7Identity.Pages
{
    [Authorize]
    public class ResourceBasedModel : PageModel
    {
        private readonly IAuthorizationService _authorizationService;
        public ResourceBasedModel(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        public bool Succeeded1 { get; set; }
        public bool Succeeded2 { get; set; }
        public async Task OnGet()
        {
           Succeeded1 = (await _authorizationService
            .AuthorizeAsync(User, "test1@gmail.com", Operations.Read)).Succeeded;

            Succeeded2 = (await _authorizationService
            .AuthorizeAsync(User, "test@gmail.com", "EditPolicy")).Succeeded;
        }
    }

    public class AuthorizationHandlerCustom1 :
    AuthorizationHandler<OperationAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       OperationAuthorizationRequirement requirement,
                                                       string resource)
        {
            if (context.User.Identity?.Name == resource &&
                requirement.Name == Operations.Read.Name)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
    public class AuthorizationHandlerCustom2 :
    AuthorizationHandler<SameAuthorRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       SameAuthorRequirement requirement,
                                                       string resource)
        {
            if (context.User.Identity?.Name == resource)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class SameAuthorRequirement : IAuthorizationRequirement { }
    public static class Operations
    {
        public static OperationAuthorizationRequirement Create =
            new() { Name = nameof(Create) };
        public static OperationAuthorizationRequirement Read =
            new() { Name = nameof(Read) };
        public static OperationAuthorizationRequirement Update =
            new() { Name = nameof(Update) };
        public static OperationAuthorizationRequirement Delete =
            new() { Name = nameof(Delete) };
    }
}
