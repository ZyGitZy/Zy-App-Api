using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.App.Common.AppExtensions
{
    public class ObjectToJsonConverter<T> : ITypeConverter<T, string>
    {
        public string Convert(T source, string destination, ResolutionContext context)
        {

            if (source == null)
            {
                return destination;
            }
            var settings = new Newtonsoft.Json.JsonSerializerSettings()
            {
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
            };

            return Newtonsoft.Json.JsonConvert.SerializeObject(source, settings);
        }
    }
}
