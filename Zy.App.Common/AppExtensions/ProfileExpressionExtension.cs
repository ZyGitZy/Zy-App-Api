using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.App.Common.AppExtensions
{
    public static class ProfileExpressionExtension
    {
        public static void CreateMapObjectToJson<TObject>(this IProfileExpression profile) where TObject : class, new()
        {
            profile.CreateMap<string, TObject>().ConvertUsing(new JsonToObjectConverter<TObject>());
            profile.CreateMap<TObject, string>().ConvertUsing(new ObjectToJsonConverter<TObject>());
        }

        public static void CreateMapDicToJson<TData>(this IProfileExpression profile)
        {
            profile.CreateMap<string, IDictionary<string, TData>>().ConvertUsing(new JsonToDicConverter<TData>());
            profile.CreateMap<IDictionary<string, TData>, string>().ConvertUsing(new DicToJsonConverter<TData>());
        }
    }
}
