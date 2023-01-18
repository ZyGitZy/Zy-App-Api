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
        public static ServiceResult<T> Error<T>(
            this IService service,
            string type,
            string? title = null,
            string? detail = null,
            Exception? exception = null)
        {
            return ServiceResult<T>.Error(type, title, detail, exception);
        }

        public static ServiceResult Error(
            this IService service,
            string type,
            string title = "",
            string detail = "",
            Exception? exception = null)
        {
            return ServiceResult.Error(type, title, detail, exception);
        }

        public static ServiceResult<T> NotFound<T>(
            this IService service,
            string resourceName,
            long? id)
        {
            return ServiceErrors.NotFound(resourceName, id).As<T>();
        }

        public static ServiceResult<T> NotFound<T>(
            this IService service,
            string resourceName,
            string resourceKey)
        {
            return ServiceErrors.NotFound(resourceName, resourceKey).As<T>();
        }

        public static ServiceResult NotFound(
            this IService service,
            string resourceName,
            long? id)
        {
            return ServiceErrors.NotFound(resourceName, id);
        }

        public static ServiceResult NotFound(
            this IService service,
            string resourceName,
            string resourceKey)
        {
            return ServiceErrors.NotFound(resourceName, resourceKey);
        }

        public static ServiceResult<T> NoDuplicate<T>(
            this IService service,
            string resourceName,
            string no)
        {
            return ServiceErrors.NoDuplicate(resourceName, no).As<T>();
        }

        public static ServiceResult NoDuplicate(
            this IService service,
            string resourceName,
            string no)
        {
            return ServiceErrors.NoDuplicate(resourceName, no);
        }

        public static ServiceResult<T> NotAllowedDelete<T>(
            this IService service,
            string resourceName,
            string no)
        {
            return ServiceErrors.NotAllowedDelete(resourceName, no).As<T>();
        }

        public static ServiceResult NotAllowedDelete(
            this IService service,
            string resourceName,
            string no)
        {
            return ServiceErrors.NotAllowedDelete(resourceName, no);
        }

        public static ServiceResult<T> NotAllowedDelete<T>(
          this IService service,
          string resourceName,
          long id)
        {
            return ServiceErrors.NotAllowedDelete(resourceName, id).As<T>();
        }

        public static ServiceResult NotAllowedDelete(
            this IService service,
            string resourceName,
            long id)
        {
            return ServiceErrors.NotAllowedDelete(resourceName, id);
        }

        public static ServiceResult<T> NotAllowedEdit<T>(
            this IService service,
            string resourceName,
            string no)
        {
            return ServiceErrors.NotAllowedEdit(resourceName, no).As<T>();
        }

        public static ServiceResult NotAllowedEdit(
            this IService service,
            string resourceName,
            string no)
        {
            return ServiceErrors.NotAllowedEdit(resourceName, no);
        }

        public static ServiceResult<T> NotAllowedEdit<T>(
          this IService service,
          string resourceName,
          long id)
        {
            return ServiceErrors.NotAllowedEdit(resourceName, id).As<T>();
        }

        public static ServiceResult NotAllowedEdit(
            this IService service,
            string resourceName,
            long id)
        {
            return ServiceErrors.NotAllowedEdit(resourceName, id);
        }

        public static ServiceResult<T> Ok<T>(this IService service, T data)
        {
            return ServiceResult<T>.Ok(data);
        }

        public static ServiceResult Ok(this IService service)
        {
            return ServiceResult.Ok();
        }

        public static Error Format(this Error error, params object[] args)
        {
            if (string.IsNullOrWhiteSpace(error.Detail))
            {
                return new Error(error.Type, string.Format(error.Title, args));
            }
            else
            {
                return new Error(error.Type, error.Title, string.Format(error.Detail, args));
            }
        }
    }
}
