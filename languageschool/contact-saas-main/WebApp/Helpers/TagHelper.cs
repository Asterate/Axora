using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebApp.Helpers;

[HtmlTargetElement("*", Attributes = "asp-authorize-roles")]
public class AuthorizeRolesTagHelper : TagHelper
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorizeRolesTagHelper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    [HtmlAttributeName("asp-authorize-roles")]
    public string Roles { get; set; } = "";

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (_httpContextAccessor.HttpContext != null)
        {
            var user = _httpContextAccessor.HttpContext.User;
            var allowed = Roles.Split(',').Any(r => user.IsInRole(r.Trim()));
            if (!allowed)
            {
                output.SuppressOutput(); // hide element if user is not in role
            }
        }
    }
}