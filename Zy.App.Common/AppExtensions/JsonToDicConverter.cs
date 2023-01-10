using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.App.Common.AppExtensions
{
    public class JsonToDicConverter<T> : ITypeConverter<string, IDictionary<string, T>>
    {
        public IDictionary<string, T> Convert(string source, IDictionary<string, T> destination, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source))
            {
                return default!;
            }

            var destinationDic = Newtonsoft.Json.JsonConvert.DeserializeObject<IDictionary<string, T>>(source) ?? new Dictionary<string, T>();
            return destinationDic;
        }
    }
}
