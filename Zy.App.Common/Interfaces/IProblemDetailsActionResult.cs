using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Interfaces;

namespace Zy.App.Common.Models
{
    public interface IProblemDetailsActionResult : IClientErrorActionResult
    {
        IServiceProblemDetails ServiceProblemDetails { get; }
    }
}
