using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.AppExtensions;
using Zy.App.Common.Models;

namespace Zy.App.Common.Core.AppAbstractions.AppAbstractionsOptions
{
    public class ZyApiExceptionAttribute : Attribute, IExceptionFilter, IAsyncExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            OnExceptionInternal(context);
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            OnExceptionInternal(context);

            return Task.CompletedTask;
        }

        private void OnExceptionInternal(ExceptionContext context)
        {
            if (context.Exception is DbUpdateConcurrencyException)
            {
                context.Result = new ProblemDetailsActionResult(new ServiceProblemDetails(AppErrorCodes.UpdateConcurrency));
            }
            else
            {
                var exception = context.Exception;
                var loggerFactory = (ILoggerFactory)context.HttpContext.RequestServices.GetRequiredService(typeof(ILoggerFactory));
                var logging = loggerFactory.CreateLogger<ZyApiExceptionAttribute>();
                logging.LogCritical(exception, "未处理异常");
                var hostEnvironment = (IHostEnvironment)context.HttpContext.RequestServices.GetRequiredService(typeof(IHostEnvironment));
                var problemDetails = new ServiceProblemDetails(AppErrorCodes.UnhandledException);
                if (!hostEnvironment.IsProduction())
                {
                    problemDetails.Detail = exception.ToString();
                }
                context.Result = new ProblemDetailsActionResult(problemDetails);
            }
        }
    }
}
