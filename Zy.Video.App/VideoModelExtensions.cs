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
        public static IMvcBuilder AddVideoServiceModule(this IMvcBuilder mvcBuilder, Action<IServiceProvider, IMapperConfigurationExpression>? mapperConfig = null)
        {
            AddControllers(mvcBuilder);
            AddScop(mvcBuilder.Services);
            mvcBuilder.Services.AddAutoMapperModule(new List<Assembly>
            {
                typeof(VideoFileProfileDto).Assembly
            });
            return mvcBuilder;
        }

        public static void AddControllers(IMvcBuilder builder)
        {
            builder.AddApplicationPart(typeof(VideoFileController).Assembly);
        }

        public static void AddScop(IServiceCollection services)
        {
            services.AddScoped<IVideoFileService, VideoFileService>();
        }
    }
}
