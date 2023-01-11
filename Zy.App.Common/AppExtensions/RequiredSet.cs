using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Zy.App.Common.AppExtensions
{
    public class RequiredSet : RequiredAttribute
    {
        private readonly HttpMethodValidator httpMethodValidator;

        public RequiredSet()
        {
            string[] methods = new string[] { HttpMethods.Patch, HttpMethods.Put };
            this.httpMethodValidator = new HttpMethodValidator(methods);
        }

        public RequiredSet(string userMethods)
        {
            this.httpMethodValidator = new HttpMethodValidator(userMethods);
        }


        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null && this.httpMethodValidator.Contains(validationContext))
            {
                return ValidationResult.Success;
            }

            return base.IsValid(value, validationContext);
        }
    }
}
