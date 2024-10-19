using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Middleware;
public class HandleErrors : IMiddleware
{
    private readonly ILogger<HandleErrors> _logger;

    public HandleErrors(ILogger<HandleErrors> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            ProblemDetails problemDetails = new()
            {
                Title = "An error occurred",
                Detail = ex.Message,
                Status = (int)HttpStatusCode.InternalServerError,
                Instance = context.Request.Path
            };
            string result = JsonSerializer.Serialize(problemDetails);
            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsync(result);
        }
    }
}
