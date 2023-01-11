using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Interfaces;

namespace Zy.App.Common.AppExtensions
{
    public static class ResourceQueryExtension
    {
        public static int GetValidOffset(this IQueryPaging paging)
        {
            if (!paging.Offset.HasValue)
            {
                return 0;
            }

            return Math.Max(paging.Offset.Value, 0);
        }

        public static int GetValidLimit(this IQueryPaging paging)
        {
            if (!paging.Limit.HasValue || paging.Limit <= 0)
            {
                if (paging.DefaultPageSize > 0)
                {
                    return paging.DefaultPageSize;
                }

                return 1;
            }

            return Math.Max(Math.Max(paging.MinPageSize, 1), Math.Min(paging.Limit.Value, paging.MaxPageSize));
        }
    }
}
