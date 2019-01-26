using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace Cats.Infrastructure
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger _logger;

        public ExceptionFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ExceptionFilter>();
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ApplicationException)
            {
                _logger.LogError(context.Exception.GetType().ToString(), context.Exception);
                context.Result = new JsonResult(context.Exception)
                {
                    Value = "Unexpected error occured",
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
