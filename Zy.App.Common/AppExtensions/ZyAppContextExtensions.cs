using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Interfaces;
using Zy.App.Common.Models;

namespace Zy.App.Common.AppExtensions
{
    public static class ZyAppContextExtensions
    {
        public static readonly string DeletedDataQueryType = "DeletedDataQueryType";

        public static DeletedDataQueryTypes GetDeletedDataQueryTypes(this IZyAppContext content)
        {
            return content.GetValue(DeletedDataQueryType, DeletedDataQueryTypes.OnlyUnDeleted);
        }

        public static DeletedDataQueryTypes GetDeletedDataQueryType(this IZyAppContext byzanContext)
        {
            return byzanContext.GetValue(DeletedDataQueryType, DeletedDataQueryTypes.OnlyUnDeleted);
        }

        public static void SetDeletedDataQueryType(this IZyAppContext byzanContext, DeletedDataQueryTypes value)
        {
            byzanContext.SetValue(DeletedDataQueryType, value);
        }

        public static void SetValue<T>(this IZyAppContext byzanContext, string key, T value)
        {
            if (byzanContext.Properties == null)
            {
                throw new Exception("IByzanContext.Properties 不能为null.");
            }

            if (!byzanContext.Properties.ContainsKey(key))
            {
                byzanContext.Properties.Add(key, value!);
            }
            else
            {
                byzanContext.Properties[key] = value!;
            }
        }

        public static T GetValue<T>(this IZyAppContext context, string key, T? defaultValue = default) where T : new()
        {
            if (context.Properties == null || !context.Properties.ContainsKey(key))
            {
                return defaultValue ?? new T();
            }

            return (T)context.Properties[key];
        }
    }
}
