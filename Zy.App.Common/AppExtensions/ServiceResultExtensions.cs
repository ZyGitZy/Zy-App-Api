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
        public static ServiceResult<T> Ok<T>(this IService service)
        {
            return ServiceResult<T>.Ok();
        }

        public static ServiceResult Ok(this IService service)
        {
            return ServiceResult.Ok();
        }

        public static ServiceResult<T> Ok<T>(this IService service, T t)
        {
            return ServiceResult<T>.Ok(t);
        }

        public static ServiceResult Error(this IService service, string detail)
        {
            return ServiceResult.Error(detail);
        }

        public static ServiceResult Error(this IService service, string titlt, string detail)
        {
            return ServiceResult.Error(titlt, detail);
        }

        public static ServiceResult<T> Error<T>(this IService service, string detail)
        {
            return ServiceResult<T>.Error(detail);
        }

        public static ServiceResult<T> Error<T>(this IService service, string title, string detail)
        {
            return ServiceResult<T>.Error(title, detail);
        }

        public static ServiceResult NoDuplicate(this IService service, string name, string no)
        {
            return ServiceResult.Error(ConcatDuplicate(name, no));
        }

        public static ServiceResult<T> NoDuplicate<T>(this IService service, string name, string no)
        {
            return ServiceResult<T>.Error(ConcatDuplicate(name, no));
        }

        public static ServiceResult NotFound(this IService service, string name, string no)
        {
            return ServiceResult.Error(ConcatNotFound(name, no));
        }

        public static ServiceResult<T> NotFound<T>(this IService service, string name, string no)
        {
            return ServiceResult<T>.Error(ConcatNotFound(name, no));
        }

        private static string ConcatDuplicate(string name, string no)
        {
            return $"{name}编号：{no}以存在!";
        }

        private static string ConcatNotFound(string name, string no)
        {
            return $"{name}：{no}不存在!";
        }
    }
}
