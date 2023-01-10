using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.App.Common.AppExtensions
{
    public class DicToJsonConverter<T> : ITypeConverter<IDictionary<string, T>, string>
    {
        public string Convert(IDictionary<string, T> source, string destination, ResolutionContext context)
        {
            if (source == null)
            {
                return destination;
            }

            IDictionary<string, T>? destinationDic = null;

            if (!string.IsNullOrWhiteSpace(destination))
            {
                destinationDic = Newtonsoft.Json.JsonConvert.DeserializeObject<IDictionary<string, T>>(destination);
            }

            destinationDic = destinationDic ?? new Dictionary<string, T>();
            foreach (var item in source)
            {
                if (item.Value == null)
                {
                    destinationDic.Remove(item.Key);
                    continue;
                }

                if (destinationDic.ContainsKey(item.Key))
                {
                    destinationDic[item.Key] = item.Value;
                }
                else
                {
                    destinationDic.Add(item.Key, item.Value);
                }
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(destinationDic);
        }
    }
}
