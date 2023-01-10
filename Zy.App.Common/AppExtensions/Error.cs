using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.App.Common.AppExtensions
{
    public class Error
    {
        public const char Split = '|';

        public Error(string type, string detail) : this(string.Empty, type, detail)
        {

        }

        public Error(string detail) : this(string.Empty, string.Empty, detail)
        {

        }

        public Error(string title, string type, string detail)
        {
            this.Title = title;
            this.Type = type;
            this.Detail = detail;
        }

        public string Title { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string Detail { get; set; } = string.Empty;

        public static explicit operator Error(string error)
        {
            if (error == null)
            {
                return new Error(string.Empty, string.Empty);
            }

            var items = error.Split(new char[] { Split }, System.StringSplitOptions.RemoveEmptyEntries);
            var type = items[0];
            var title = string.Empty;
            var details = string.Empty;
            if (items.Length > 1)
            {
                title = items[1];
                if (items.Length > 2)
                {
                    details = items[2];
                }
            }
            return new Error(type, title, details);
        }

        public static implicit operator string(Error errorCode)
        {
            return string.Concat(errorCode.Type, Split, errorCode.Title, Split, errorCode.Detail);
        }

        public static implicit operator ServiceResult<long>(Error errorCode)
        {
            return ServiceResult<long>.Error(errorCode.Type, errorCode.Title, errorCode.Detail);
        }

        public static implicit operator ServiceResult(Error error)
        {
            return ServiceResult.Error(error.Type, error.Title, error.Detail);
        }

        public ServiceResult<T> As<T>()
        {
            return ServiceResult<T>.Error(this.Type, this.Title, this.Detail);
        }
    }
}
