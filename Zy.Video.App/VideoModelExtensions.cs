using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Zy.App.Common.AppExtensions;
using Zy.App.Common.Core.AppAbstractions.IAppAbstractionsOptions;
using Zy.App.Common.Interfaces;
using Zy.App.Common.Models;
using Zy.Video.App.Controllers;
using Zy.Video.App.Profiles;
using Zy.Video.Bll.Interfaces;
using Zy.Video.Bll.Services;

namespace Zy.Video.App
{
    public static class VideoModelExtensions
    {
        public static IZyMvcBuilder AddVideoServiceModule(this IZyMvcBuilder mvcBuilder, Action<IServiceProvider, IMapperConfigurationExpression>? mapperConfig = null)
        {

            mvcBuilder.AddModules(m =>
            {
                m.AddAutoMapper(typeof(VideoFileProfileDto).Assembly);
                m.AddController(typeof(VideoFileController).Assembly);
                m.AddService();
            });

            return mvcBuilder;
        }

        private static void AddService(this IZyMvcModuleBuilder services)
        {
            services.Services.AddScoped<IVideoFileService, VideoFileService>();
        }
    }
}
