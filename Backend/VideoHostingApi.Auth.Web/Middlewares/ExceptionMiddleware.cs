using System.Text.Json;
using VideoHostingApi.Auth.Services.Contracts.Exceptions;
using VideoHostingApi.Common.Web.Models;

namespace VideoHostingApi.Auth.Web.Middlewares;

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

                case EntityNotFoundException entityNotFoundException: 
                    response.StatusCode = StatusCodes.Status404NotFound;
                    error.Message = entityNotFoundException.Message;
                    break;
                
                case EntityIsExistException entityIsExistException: 
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    error.Message = entityIsExistException.Message;
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