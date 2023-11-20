using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Exceptions;
using Serilog.Formatting.Compact;
using Serilog.Sinks.Elasticsearch;
using System.Globalization;
using System.Reflection;

namespace Tools.Logging;
public static class LoggingInstaller {
    public static IServiceCollection AddSerilogLogging(this IServiceCollection services, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        return services;
    }

    /// <summary>
    /// Serilog include elasticsearch
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddSerilogLogging(this IServiceCollection services)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile(
                $"appsettings.{environment}.json", optional: false
            )
            .Build();

        var elasticConfiguration = new ElasticConfiguration();
        configuration.Bind("ElasticConfiguration", elasticConfiguration);

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(
                path: "/logs/log-.txt",
                rollingInterval: RollingInterval.Day,
                rollOnFileSizeLimit: true,
                formatter: new CompactJsonFormatter()
            )
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticConfiguration.Uri))
            {
                AutoRegisterTemplate = true
            })
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        return services;
    }
}