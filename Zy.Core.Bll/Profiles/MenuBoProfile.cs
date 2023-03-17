using AutoMapper;
using IdentityServer4.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.AppExtensions;
using Zy.Core.Bll.Modelus;
using Zy.Core.Dal.Entitys;
using static AutoMapper.Internal.ExpressionFactory;

namespace Zy.Core.Bll.Profiles
{
    public class MenuBoProfile : Profile
    {
        public MenuBoProfile()
        {
            this.CreateMap<MenuBo, MenuEntity>()
            .ForMember(f => f.FullPath, b => b.MapFrom(f => f.FullPath.Any() ? JsonConvert.SerializeObject(f.FullPath) : "[]"));

            this.CreateMap<MenuEntity, MenuBo>().ForMember(f => f.FullPath, b => b.MapFrom(f => JsonConvert.DeserializeObject<List<long>>(f.FullPath)));
        }
    }
    public static class Test
    {
        public static IMappingExpression<bo, entity> ConvertToString<bo, entity>(this IMappingExpression<bo, entity> mappingExpression, Func<entity, string> action, Func<bo, List<long>> action1)
        {
            mappingExpression.ForMember(f => action(f), b => b.MapFrom(f => action1(f).Any() ? JsonConvert.SerializeObject(action1(f)) : "[]"));

            return mappingExpression;
        }

        public static IMappingExpression<entity, bo> ConvertToList<entity, bo>(this IMappingExpression<entity, bo> mappingExpression, Func<bo, string> action, Func<entity, string> action1)
        {
            mappingExpression.ForMember(f => action(f), b => b.MapFrom(f => action1(f).Any() ? JsonConvert.DeserializeObject<List<long>>(action1(f)) : new List<long>()));

            return mappingExpression;
        }
    }
}
