using ForumAPI.Contract.ResponseContract;
using ForumAPI.Service.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace ForumAPI.WebAPI.Middleware
{
    public static class UseCustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var statusCode = exceptionFeature.Error switch
                    {
                        ClientSideException => HttpStatusCode.BadRequest,
                        NotFoundException => HttpStatusCode.NotFound,
                        _ => HttpStatusCode.InternalServerError
                    };
                    context.Response.StatusCode = (int)statusCode;
                    var response = CustomResponseContract.Fail(exceptionFeature.Error.Message, statusCode);
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                });
            });
        }
    }
}
