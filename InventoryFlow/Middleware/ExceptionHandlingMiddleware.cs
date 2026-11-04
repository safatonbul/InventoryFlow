using Azure;

namespace InventoryFlow.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch (Exception ex)
            {
                if (ex is InvalidOperationException)
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("{\"message\":\"Stock operation is invalid.\"}");
                }
                else
                {
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync("{\"message\":\"An unexpected error occurred.\"}");
                }
                context.Response.ContentType = "application/json";
                
            }
        }
    }
}
