using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using OpenTelemetry.Trace;
using OpenTelemetry.Resources;

namespace TestRedisInstrumentationApi
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            // https://github.com/open-telemetry/opentelemetry-dotnet/blob/main/src/OpenTelemetry.Instrumentation.StackExchangeRedis/README.md
            // https://blog.sivamuthukumar.com/open-telemetry-observability-in-net-core-apps

            var redisConnection = ConnectionMultiplexer.Connect($"localhost:6379");

            services.AddOpenTelemetryTracing((builder) => 
                builder
                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("TestRedis"))
                    .AddAspNetCoreInstrumentation()
                    .AddRedisInstrumentation(redisConnection)
                    .AddZipkinExporter(opt => {
                        opt.Endpoint = new System.Uri($"http://localhost:9411/api/v2/spans");
                    })
            );

            services.AddSingleton<IConnectionMultiplexer>(redisConnection);
            return services; 
        }
    }
}
