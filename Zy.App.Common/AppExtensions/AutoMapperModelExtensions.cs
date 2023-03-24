using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Core.AppAbstractions.IAppAbstractionsOptions;
using Zy.App.Common.Core.DbContextExtension;
using Zy.App.Common.Core.DbContextExtension.ZyDbContextOptions;
using Zy.App.Common.Interfaces;
using Zy.App.Common.Models;
using Zy.App.Common.StoreCore;
using static System.Collections.Specialized.BitVector32;

namespace Zy.App.Common.AppExtensions
{
    public static class AutoMapperModelExtensions
    {
        public static IServiceCollection AddAutoMapperModule(this IServiceCollection services, IEnumerable<Assembly> autoAssemblies, Action<IServiceProvider, IMapperConfigurationExpression>? mapperConfig = null)
        {
            services.AddAutoMapper((sp, mapConfig) =>
            {
                if (mapperConfig != null)
                {
                    mapperConfig.Invoke(sp, mapConfig);
                }
                mapConfig.AllowNullCollections = true;
           
                var c = mapConfig.ValueTransformers;

                mapConfig.ForAllMaps((typeMap, mappingExpression) =>
                {
                    var isBo = typeof(ResourceBo).IsAssignableFrom(typeMap.SourceType);
                    var isEntity = typeof(IEntity).IsAssignableFrom(typeMap.DestinationType);

                    if (isBo && isEntity)
                    {
                        mappingExpression.ForMember(nameof(EntityBase.Id), m => m.Ignore());
                        mappingExpression.ForAllOtherMembers(a => a.UseDestinationValue());
                    }
                });

                mapConfig.CreateMapDicToJson<IList<string>>();
                mapConfig.CreateMapDicToJson<object>();

            }, autoAssemblies);

            return services;
        }

        public static IZyMvcBuilder AddLibScopModels(this IZyMvcBuilder services)
        {
            services.Services.AddScoped<IZyAppContext, ZyAppContext>(sp => new ZyAppContext(sp.GetRequiredService<IHttpContextAccessor>()));
            services.Services.AddScoped(typeof(IEntityStore<>), typeof(EntityStore<>));
            services.Services.AddScoped<INoNormalizer, NoNormalizer>();

            return services;
        }
    }
}
