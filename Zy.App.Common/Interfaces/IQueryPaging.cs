using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.App.Common.Interfaces
{
    public interface IQueryPaging
    {
        int DefaultPageSize { get; }

        bool EnablePaging { get; }

        int? Limit { get; set; }

        int MaxPageSize { get; }

        int MinPageSize { get; }

        int? Offset { get; set; }
    }
}
