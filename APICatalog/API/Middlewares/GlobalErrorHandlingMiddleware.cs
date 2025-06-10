using System.Text.Json;

namespace APICatalog.API.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == StatusCodes.Status401Unauthorized ||
                context.Response.StatusCode == StatusCodes.Status403Forbidden)
            {
                if (!context.Response.HasStarted)
                {
                    context.Response.ContentType = "application/json";
                    var response = new
                    {
                        success = false,
                        statusCode = context.Response.StatusCode,
                        requestTime = DateTime.UtcNow,
                        errorType = context.Response.StatusCode == 401 ? "Unauthorized" : "Forbidden",
                        message = context.Response.StatusCode == 401
                            ? "Invalid Token or Not Found."
                            : "Permission Denied."
                    };

                    var json = JsonSerializer.Serialize(response);
                    await context.Response.WriteAsync(json);
                }
            }
        }
    }
}
