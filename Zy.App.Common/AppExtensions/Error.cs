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

        public static readonly Error NoDuplicate = new Error(nameof(NoDuplicate), "编号重复", "{0}已经存在");

        public static readonly Error NotFound = new Error(nameof(NotFound), "资源不存在", "{0}不存在");

        public static readonly Error NotAllowedDelete = new Error(nameof(NotAllowedDelete), "禁止删除", "禁止删除{0}");

        public static readonly Error NotAllowedEdit = new Error(nameof(NotAllowedEdit), "禁止修改", "禁止修改{0}");

        public Error(string type)
            : this(type, string.Empty)
        {
        }

        public Error(string type, string title)
            : this(type, title, string.Empty)
        {
        }

        public Error(string type, string title, string detail)
        {
            this.Type = type;

            this.Title = title;

            this.Detail = detail;
        }

        public string Type { get; set; }

        public string Title { get; set; }

        public string Detail { get; set; }

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
