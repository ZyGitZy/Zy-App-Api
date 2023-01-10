using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Models;

namespace Zy.App.Common.Interfaces
{
    public interface IServiceResult
    {

        public object? TData { get; set; }

        public IErrorDetail? ErrDetail { get; set; }

        public bool Success { get; set; }
    }
}
