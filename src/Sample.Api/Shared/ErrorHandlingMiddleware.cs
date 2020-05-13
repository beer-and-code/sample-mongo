using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Api.Shared
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DomainException domainException)
            {
                await HandlingError(context, domainException.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception exception)
            {
                await HandlingError(context, exception.Message, HttpStatusCode.InternalServerError);
            }
        }

        private static async Task HandlingError(HttpContext context, string message, HttpStatusCode statusCode)
        {
            var result = JsonConvert.SerializeObject(new { error = message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsync(result, CancellationToken.None);
        }
    }
}
