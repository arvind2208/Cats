using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Threading.Tasks;

namespace Cats.Infrastructure
{
    public class UnhandledExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public UnhandledExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<UnhandledExceptionMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unhandled exception");
                throw; //rethrow, do not swallow exception
            }
        }
    }
}
