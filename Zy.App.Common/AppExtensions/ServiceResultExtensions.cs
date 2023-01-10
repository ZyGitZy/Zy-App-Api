using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Models;

namespace Zy.App.Common.AppExtensions
{
    public static class ServiceResultExtensions
    {
        public static ServiceResult<T> Ok<T>(this IService service, T t)
        {
            return ServiceResult<T>.Ok(t);
        }

        public static ServiceResult<T> Error<T>(this IService service, string title, string detail)
        {
            return ServiceResult<T>.Error(title, detail);
        }
    }
}
