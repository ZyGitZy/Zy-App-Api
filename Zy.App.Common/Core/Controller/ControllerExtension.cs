using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.AppExtensions;
using Zy.App.Common.Interfaces;
using Zy.App.Common.Models;

namespace Zy.App.Common.Core.Controller
{
    public static class ControllerExtension
    {
        public static IProblemDetailsActionResult Fail(this ControllerBase controller, string message = "")
        {
            return new ProblemDetailsActionResult(new ErrorDetail(message));
        }

        public static IProblemDetailsActionResult Fail(this ControllerBase controller, string type, string message)
        {
            return new ProblemDetailsActionResult(new ErrorDetail(type, message));
        }

        public static IProblemDetailsActionResult Fail(this ControllerBase controller, string title, string type, string message)
        {
            return new ProblemDetailsActionResult(new ErrorDetail(title, type, message));
        }

        public static IProblemDetailsActionResult Fail(this ControllerBase controller, IServiceResult serviceResult)
        {
            return new ProblemDetailsActionResult(serviceResult.ErrDetail ?? new ErrorDetail(serviceResult.Success.ToString()));
        }

        public static IActionResult Result(this ControllerBase controller, IServiceResult serviceResult)
        {
            if (serviceResult.Success)
            {
                if (serviceResult.TData == null)
                {
                    return controller.NoContent();
                }

                return controller.Ok(serviceResult.TData);
            }

            return controller.Fail(serviceResult);
        }

        public static IActionResult Result<T>(this ControllerBase controller, ServiceResult<T> serviceResult, Func<T, object> mapper)
        {
            if (serviceResult.Success && serviceResult.TData != null)
            {
                var result = mapper(serviceResult.TData);
                return controller.Ok(result);
            }

            return controller.Fail(serviceResult);
        }

        public static IActionResult Result<T, S>(this ControllerBase controller, ServiceResult<QueryResult<T>> serviceResult, IMapper mapper, int count = 0)
        {
            if (serviceResult.Success && serviceResult.TData != null && serviceResult.TData.Items != null)
            {
                var result = mapper.Map<IEnumerable<T>, IEnumerable<S>>(serviceResult.TData.Items);

                QueryResult<S> queryResult = new() { Count = count, Items = result, Offset = serviceResult.TData.Offset, Limit = serviceResult.TData.Limit };

                return controller.Ok(queryResult);
            }

            return controller.Fail(serviceResult);
        }

        public static IActionResult Result<T, S>(this ControllerBase controller, ServiceResult<T> serviceResult, IMapper mapper)
        {
            if (serviceResult.Success && serviceResult.TData != null)
            {
                var result = mapper.Map<T, S>(serviceResult.TData);

                return controller.Ok(result);
            }

            return controller.Fail(serviceResult);
        }
    }
}
