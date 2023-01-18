using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Interfaces;
using Zy.App.Common.Models;

namespace Zy.App.Common.AppExtensions
{
    public class ProblemDetailsActionResult : IProblemDetailsActionResult
    {
        public ProblemDetailsActionResult(IServiceProblemDetails errorDetail)
        {
            this.ServiceProblemDetails = errorDetail;
        }

        public IServiceProblemDetails ServiceProblemDetails { get; private set; }

        public int? StatusCode => this.ServiceProblemDetails.Status;

        public Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(this.ServiceProblemDetails)
            {
                StatusCode = this.ServiceProblemDetails.Status ?? StatusCodes.Status400BadRequest
            };

            return objectResult.ExecuteResultAsync(context);
        }
    }
}
