using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Interfaces;
using Zy.App.Common.Models;

namespace Zy.App.Common.AppExtensions
{
    public class ServiceResult<T> : IServiceResult
    {
        public T? TData { get; set; }

        public IErrorDetail? ErrDetail { get; set; }

        public bool Success { get; set; }

        object? IServiceResult.TData { get => this.TData; set => this.TData = (T)(value ?? default!); }

        public static ServiceResult<T> Ok()
        {
            return new ServiceResult<T> { Success = true };
        }

        public static ServiceResult<T> Ok(T t)
        {
            return new ServiceResult<T> { Success = true, TData = t };
        }

        public static ServiceResult<T> Error(string type, string title, string detail)
        {
            IErrorDetail info = new ErrorDetail(title, type, detail);
            return new ServiceResult<T> { Success = false, TData = default, ErrDetail = info };
        }

        public static ServiceResult<T> Error(string title, string detail)
        {
            IErrorDetail info = new ErrorDetail(title, detail);
            return new ServiceResult<T> { Success = false, TData = default, ErrDetail = info };
        }

        public static ServiceResult<T> Error(string detail)
        {
            IErrorDetail info = new ErrorDetail(detail);
            return new ServiceResult<T> { Success = false, TData = default, ErrDetail = info };
        }
    }
}
