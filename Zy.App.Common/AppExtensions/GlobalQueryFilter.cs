using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Interfaces;
using Zy.App.Common.Models;

namespace Zy.App.Common.AppExtensions
{
    public class GlobalQueryFilter
    {
        private IZyAppContext singlarContext;

        public GlobalQueryFilter()
        {
            this.singlarContext = EmptyZyAppContext.Empty;
        }

        public GlobalQueryFilter(IZyAppContext singlarContext)
        {
            this.singlarContext = singlarContext;
        }

        public virtual bool IgnoreDeleted => this.singlarContext.GetDeletedDataQueryType() == DeletedDataQueryTypes.All;

        public virtual bool Deleted => this.singlarContext.GetDeletedDataQueryType() == DeletedDataQueryTypes.OnlyDeleted;
    }
}
