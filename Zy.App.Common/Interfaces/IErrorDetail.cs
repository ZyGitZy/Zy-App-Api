using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.App.Common.Models
{
    public interface IErrorDetail
    {
        string Message { get; set; }

        string Type { get; set; }

        int? Status { get; set; }

        string Title { get; set; }
    }
}
