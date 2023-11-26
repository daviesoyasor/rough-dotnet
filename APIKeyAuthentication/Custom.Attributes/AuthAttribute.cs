using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Polaris.APIKeyAuthentication.Custom.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class AuthAttribute : Attribute, IAuthorizationFilter
    {
        //As you can see, the following class is inheriting from the Attribute class and implementing IAuthorizationFilter interface
        //so we can use this class as an attribute and a filter

        private const string ApiKeyHeaderName = "X-API-Key";
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string apiKey = context.HttpContext.Request.Headers[ApiKeyHeaderName];
            if (apiKey == null)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
