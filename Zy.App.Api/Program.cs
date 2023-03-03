using Zy.App.Common.StoreCore;
using Zy.User.App;
using Zy.Video.App;
using Zy.Ids.App;
using Zy.App.Common.AppExtensions;
using Zy.App.Api;
using NLog.Extensions.Hosting;

public class Program
{
    public static void Main(string[] args)
    {
        CreateWebHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateWebHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(builder =>
        {
            builder.UseStartup<Startup>();
        }).UseNLog();

}

