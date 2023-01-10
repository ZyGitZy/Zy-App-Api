using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Models;

namespace Zy.App.Common.AppExtensions
{
    public class ErrorDetail : IErrorDetail
    {

        public ErrorDetail(string title, string type, string message)
        {
            this.Message = message;
            this.Type = type;
            this.Title = title;
        }

        public ErrorDetail(string type, string message) : this("", type, message)
        {
            this.Message = message;
            this.Type = type;
        }

        public ErrorDetail(string message) : this("", "", message)
        {
        }

        public string Message { get; set; }
        public string Type { get; set; }
        public int? Status { get; set; }
        public string Title { get; set; }
    }
}
