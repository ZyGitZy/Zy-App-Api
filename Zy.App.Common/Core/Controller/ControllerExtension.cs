using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.AppExtensions;
using Zy.App.Common.Interfaces;
using Zy.App.Common.Models;
using ValidationProblemDetails = Zy.App.Common.AppExtensions.ValidationProblemDetails;

namespace Zy.App.Common.Core.Controller
{
    public static class ControllerExtension
    {
        public static IProblemDetailsActionResult Fail(this ControllerBase controller, string title = "")
        {
            return new ProblemDetailsActionResult(new ServiceProblemDetails(title));
        }

        public static IProblemDetailsActionResult Fail(this ControllerBase controller, string type, string title)
        {
            return new ProblemDetailsActionResult(new ServiceProblemDetails(type, title));
        }

        public static IProblemDetailsActionResult Fail(this ControllerBase controller, IServiceResult serviceResult)
        {
            return new ProblemDetailsActionResult(serviceResult.ProblemDetails ??
                                                  new ServiceProblemDetails(serviceResult.Success.ToString()));
        }

        public static IProblemDetailsActionResult Fail(this ControllerBase controller, ModelStateDictionary modelState)
        {
            var problemDetails = new ValidationProblemDetails(modelState);
            return new ProblemDetailsActionResult(problemDetails);
        }

        public static string GetClientIpWithPort(this ControllerBase controller)
        {
            var ipAddress = controller.Request.HttpContext.Connection.RemoteIpAddress;
            var port = controller.Request.HttpContext.Connection.RemotePort;

            return $"{ipAddress}:{port}";
        }

        public static string GetServerIpWithPort(this ControllerBase controller)
        {
            var ipAddress = controller.Request.HttpContext.Connection.LocalIpAddress;
            var port = controller.Request.HttpContext.Connection.LocalPort;
            return $"{ipAddress}:{port}";
        }

        public static string GetUserAgent(this ControllerBase controller)
        {
            if (!controller.Request.Headers.TryGetValue("useragent", out var value))
            {
                return string.Empty;
            }

            return value.ToString();
        }

        public static IActionResult Result(this ControllerBase controller, IServiceResult serviceResult)
        {
            if (serviceResult.Success)
            {
                if (serviceResult.Data == null)
                {
                    return controller.NoContent();
                }

                return controller.Ok(serviceResult.Data);
            }

            return controller.Fail(serviceResult);
        }

        public static IActionResult Result<T>(
         this ControllerBase controller,
         ServiceResult<T> serviceResult,
         Func<T, object> mapper)
        {
            if (serviceResult.Success)
            {
                var responseData = mapper(serviceResult.Data);
                return controller.Ok(responseData);
            }

            return controller.Fail(serviceResult);
        }

        public static IActionResult Result<TSource, TDestination>(
            this ControllerBase controller,
            ServiceResult<TSource> serviceResult,
            IMapper mapper)
        {
            if (serviceResult.Success)
            {
                var responseData = mapper.Map<TSource, TDestination>(serviceResult.Data);
                return controller.Ok(responseData);
            }

            return controller.Fail(serviceResult);
        }

        public static IActionResult Result<TSource, TDestination>(
            this ControllerBase controller,
            ServiceResult<QueryResult<TSource>> serviceResult,
            IMapper mapper)
        {
            if (serviceResult.Success)
            {
                var responseData =
                    mapper.Map<IEnumerable<TSource>?, IEnumerable<TDestination>>(serviceResult.Data.Items);
                return controller.ResultList(responseData, serviceResult.Data.Count, serviceResult.Data.Offset, serviceResult.Data.Limit);
            }

            return controller.Fail(serviceResult);
        }

        public static IActionResult ResultList<TSource, TDestination>(
            this ControllerBase controller,
            ServiceResult<IEnumerable<TSource>> serviceResult,
            IMapper mapper,
            int? count = null,
            int? offset = null,
            int? limit = null)
        {
            if (serviceResult.Success)
            {
                var responseData = mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(serviceResult.Data);
                return controller.ResultList(responseData, count, offset, limit);
            }

            return controller.Fail(serviceResult);
        }

        public static IActionResult ResultList(
            this ControllerBase controller,
            IEnumerable dataList,
            int? count = null,
            int? offset = null,
            int? limit = null)
        {
            var queryResponseDto =
                new QueryResponseDto { Items = dataList, Count = count, Offset = offset, Limit = limit };
            return controller.Ok(queryResponseDto);
        }

        public static IActionResult ResultList<T>(
            this ControllerBase controller,
            IEnumerable<T> dataList,
            int? count = null,
            int? offset = null,
            int? limit = null)
        {
            var queryResponseDto =
                new QueryResponseDto<T> { Items = dataList, Count = count, Offset = offset, Limit = limit };
            return controller.Ok(queryResponseDto);
        }
    }
}
