using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.App.Common.AppExtensions
{
    public class HttpMethodValidator
    {
        public HttpMethodValidator()
        {
            this.HttpMethods = new HashSet<string>();
        }

        public HttpMethodValidator(string httpMethods)
        {
            this.HttpMethods = new HashSet<string>();
            if (string.IsNullOrWhiteSpace(httpMethods))
            {
                return;
            }
            var httpMethodsArray = httpMethods.Split(new char[] { ',', '，', ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in httpMethodsArray)
            {
                var method = this.NormalizeMethod(item);
                if (string.IsNullOrEmpty(method))
                {
                    continue;
                }
                this.HttpMethods.Add(method);
            }
        }

        public HttpMethodValidator(string[] httpMethods)
        {
            this.HttpMethods = new HashSet<string>();
            if (httpMethods == null)
            {
                return;
            }

            foreach (var item in httpMethods)
            {
                var method = this.NormalizeMethod(item);

                if (string.IsNullOrEmpty(method))
                {
                    continue;
                }
                this.HttpMethods.Add(method);
            }
        }

        public HashSet<string> HttpMethods { get; }

        public bool Contains(ValidationContext validationContext)
        {
            if (this.HttpMethods.Count == 0)
            {
                return false;
            }

            var httpContextAccessor = validationContext.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;
            if (httpContextAccessor == null)
            {
                throw new ArgumentNullException("IHttpContextAccessor is null.");
            }
            var requestMethod = this.NormalizeMethod(httpContextAccessor?.HttpContext?.Request?.Method);
            if (requestMethod == null)
            {
                return false;
            }
            return this.HttpMethods.Contains(requestMethod);
        }

        private string? NormalizeMethod(string? method)
        {
            return method?.Trim().ToLower();
        }
    }
}
