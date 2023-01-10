using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Models;

namespace Zy.App.Common.AppExtensions
{
    public class ProblemDetailsActionResult : IProblemDetailsActionResult
    {
        public ProblemDetailsActionResult(IErrorDetail errorDetail)
        {
            this.errorDetail = errorDetail;
        }

        public IErrorDetail errorDetail { get; private set; }

        public int? StatusCode => this.errorDetail.Status;

        public Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(this.errorDetail)
            {
                StatusCode = this.errorDetail.Status ?? StatusCodes.Status400BadRequest
            };

            return objectResult.ExecuteResultAsync(context);
        }
    }
}
