using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.Interfaces;
using Zy.App.Common.Models;
using Zy.App.Common.StoreCore;

namespace Zy.App.Common.AppExtensions
{
    public static class AutoMapperModelExtensions
    {
        public static IServiceCollection AddAutoMapperModule(this IServiceCollection services, List<Assembly> autoAssemblies, Action<IServiceProvider, IMapperConfigurationExpression>? mapperConfig = null)
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
            }, autoAssemblies);

            return services;
        }

        public static IServiceCollection AddLibScopModels(this IServiceCollection services)
        {
            services.AddScoped<INoNormalizer, NoNormalizer>();
            services.AddScoped(typeof(IEntityStore<>), typeof(EntityStore<>));

            return services;
        }
    }
}
