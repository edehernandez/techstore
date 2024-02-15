using Catalog.API.Exceptions;
using Newtonsoft.Json;

namespace Catalog.API.Middlewares
{
    public class InvalidModelMiddleware
    {
        private readonly RequestDelegate _next;

        public InvalidModelMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (InvalidModelException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    status = "error",
                    message = "Validation failed",
                    errors = ex.Errors
                };

                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }
        }
    }
}
