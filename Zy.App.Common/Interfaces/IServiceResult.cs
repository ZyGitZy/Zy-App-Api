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
        object? Data { get; set; }

        IServiceProblemDetails? ProblemDetails { get; set; }

        bool Success { get; set; }
    }
}
