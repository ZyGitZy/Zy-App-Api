using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.AppExtensions;

namespace Zy.App.Common.Models
{
    public static class ServiceErrors
    {
        public static Error NotFound(
           string resourceName,
           long? id)
        {
            var value = resourceName;
            if (id.HasValue)
            {
                value = $"{resourceName}Id:{id}";
            }

            return Error.NotFound.Format(value);
        }

        public static Error NotFound(
            string resourceName,
            string? resourceKey)
        {
            var value = resourceName;
            if (!string.IsNullOrEmpty(resourceKey))
            {
                value = $"{resourceName}Key:{resourceKey}";
            }

            return Error.NotFound.Format(value);
        }

        public static Error NoDuplicate(
          string resourceName,
          string no)
        {
            var value = resourceName;
            if (!string.IsNullOrWhiteSpace(no))
            {
                value = $"{resourceName}编号:{no}";
            }

            return Error.NoDuplicate.Format(value);
        }

        public static Error NotAllowedDelete(
          string resourceName,
          string no)
        {
            var value = resourceName;
            if (!string.IsNullOrWhiteSpace(no))
            {
                value = $"{resourceName}，编号:{no}";
            }

            return Error.NotAllowedDelete.Format(value);
        }

        public static Error NotAllowedDelete(
          string resourceName,
          long? id)
        {
            var value = resourceName;
            if (id.HasValue)
            {
                value = $"{resourceName}，Id:{id}";
            }

            return Error.NotAllowedDelete.Format(value);
        }

        public static Error NotAllowedEdit(
          string resourceName,
          string no)
        {
            var value = resourceName;
            if (!string.IsNullOrWhiteSpace(no))
            {
                value = $"{resourceName}，编号:{no}";
            }

            return Error.NotAllowedEdit.Format(value);
        }

        public static Error NotAllowedEdit(
          string resourceName,
          long? id)
        {
            var value = resourceName;
            if (id.HasValue)
            {
                value = $"{resourceName}，Id:{id}";
            }

            return Error.NotAllowedEdit.Format(value);
        }
    }
}
