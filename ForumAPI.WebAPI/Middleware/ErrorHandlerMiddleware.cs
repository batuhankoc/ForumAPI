using ForumAPI.Contract.ResponseContract;
using ForumAPI.Service.Exceptions;
using System.Net;
using System.Text.Json;

namespace ForumAPI.WebAPI.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                HttpStatusCode httpStatusCode;
                switch (error)
                {
                    case ClientSideException e:
                        httpStatusCode = HttpStatusCode.BadRequest;
                        break;
                    case NotFoundException e:
                        httpStatusCode = HttpStatusCode.NotFound;
                        break;
                    default:
                        httpStatusCode = HttpStatusCode.InternalServerError;
                        break;
                }
                var result = CustomResponseContract.Fail(error.Message, httpStatusCode);
                await response.WriteAsync(JsonSerializer.Serialize(result));
                _logger.LogError(error, $"Hatanın sebebi: {error.Message}");
            }
        }
    }
}
