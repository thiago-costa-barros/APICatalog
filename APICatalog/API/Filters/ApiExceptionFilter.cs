using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalog.API.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ApiExceptionFilter> _logger;
        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "An generic error occurred while processing the request.");
            context.Result = new ObjectResult(new
            {
                Success = false,
                StatusCode = StatusCodes.Status500InternalServerError,
                RequestTime = DateTime.UtcNow,
                Message = "An error occurred while processing your request. Please try again later."
            })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
