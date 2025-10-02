using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Products_Crud.Filters
{
    public class ApiKeyAuthorizationFilter : IAsyncAuthorizationFilter
    {
        private const string API_KEY = "secret123";
        public Task OnAuthorizationAsync(AuthorizationFilterContext AuthorizationFilterContextobj)
        {
            var request = AuthorizationFilterContextobj.HttpContext.Request;
            if (!request.Headers.TryGetValue(API_KEY, out var apikey))
            {
                AuthorizationFilterContextobj.Result = new UnauthorizedObjectResult("API key is missing.");
                return Task.CompletedTask;
            }
            if (apikey != API_KEY)
            {
                AuthorizationFilterContextobj.Result = new UnauthorizedObjectResult("Invalid API key.");
            }
            return Task.CompletedTask;
        }
    }
}
