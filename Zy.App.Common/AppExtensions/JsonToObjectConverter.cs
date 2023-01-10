using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.App.Common.AppExtensions
{
    public class JsonToObjectConverter<T> : ITypeConverter<string, T> where T : class, new()
    {
        public T Convert(string source, T destination, ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return default!;
            }

            var destinationObject = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(source);
            return destinationObject ?? new T();
        }
    }
}
