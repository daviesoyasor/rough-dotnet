using Microsoft.AspNetCore.Mvc;
using Polaris.APIKeyAuthentication.Filters;

namespace Polaris.APIKeyAuthentication.Custom.Attributes
{
    public class ApiKeyAttribute : ServiceFilterAttribute
    {
        public ApiKeyAttribute()
               : base(typeof(ApiKeyAuthorizationFilter))
        {
        }
    }
}
