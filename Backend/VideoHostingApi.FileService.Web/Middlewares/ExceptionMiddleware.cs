using System.Text.Json;
using Minio.Exceptions;
using ErrorResponse = VideoHostingApi.FileService.Web.Controllers.Models.ErrorResponse;

namespace VideoHostingApi.FileService.Web.Middlewares;

public class ExceptionMiddleware(RequestDelegate next)
{
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch(Exception ex)
        {
            context.Response.ContentType = "application/json";
            
            var response = context.Response;
            var error = new ErrorResponse();

            switch (ex)
            {
                case BucketNotFoundException bucketNotFoundException:
                    response.StatusCode = StatusCodes.Status404NotFound;
                    error.Message = bucketNotFoundException.Message;
                    break;
                

                default:
                    response.StatusCode = StatusCodes.Status500InternalServerError;
                    error.Message = "Server error: ";
                    error.Message += ex.Message;
                    break;
            }
            
            error.StatusCode = response.StatusCode;
            
            var json = JsonSerializer.Serialize(error);
            await context.Response.WriteAsync(json);
        }
    }
}