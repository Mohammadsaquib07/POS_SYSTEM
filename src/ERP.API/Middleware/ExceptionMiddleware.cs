using System.Net;
using System.Text.Json;

namespace Products_Crud.ExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate RequestDelegateobj,ILogger<ExceptionMiddleware> loggerObj)
        {
            _next = RequestDelegateobj;
            _logger = loggerObj;
        }

        public async Task Invoke(HttpContext httpContextObj)
        {
            try
            {
                await _next(httpContextObj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occured");
                httpContextObj.Response.ContentType = "application/json";
                httpContextObj.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = new
                {
                    status = httpContextObj.Response.StatusCode,
                    message = "Internal server error",
                    details = ex.Message
                };
                await httpContextObj.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}