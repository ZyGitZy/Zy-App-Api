using Microsoft.AspNetCore.Builder;
using Pomelo.EntityFrameworkCore.MySql.Internal;
using Zy.App.Common.AppExtensions;
using Zy.App.Common.Core.App.Abstractions;
using Zy.App.Common.Core.ApplicationBuilderExtensions;
using Zy.App.Common.Core.DbContextExtension.ZyDbContextOptions;
using Zy.App.Common.Core.HealthCheckExtensions;
using Zy.App.Common.Models;
using Zy.Core.App.Extensions;
using Zy.Ids.App;
using Zy.Ids.App.JwtModelExtensions;
using Zy.User.App;
using Zy.User.DAL.Entitys;
using Zy.Video.App;

namespace Zy.App.Api
{
    public class Startup
    {
        public IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var builder = services.AddZyMvcBuilder().AddLibScopModels();

            ZyDbContextOption option = new ZyDbContextOption();
            this.Configuration.GetSection("MySql").Bind(option);

            builder.AddIdsModel(this.Configuration, e => e.Apply(option))
                .AddUserModel(e => e.Apply(option))
            .AddVideoServiceModule()
            .AddZyCoreModule(e => e.Apply(option))
            .AddModules(m => m.AddHealthCheckMySQL(option.GetConnectionString()))
            .BuildModules();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseZyApplicationBuilder();

        }
    }
}
