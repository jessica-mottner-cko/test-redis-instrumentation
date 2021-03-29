using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace TestRedisInstrumentationApi
{
    class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults((webHostBuilder) =>
                {
                    webHostBuilder.ConfigureKestrel(x =>
                    {
                        x.AddServerHeader = false;
                    })
                    .UseStartup<Startup>();
                });
    }
}
