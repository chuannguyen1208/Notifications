﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Exceptions;
using Serilog.Formatting.Compact;
using Serilog.Sinks.Elasticsearch;
using System.Globalization;
using System.Reflection;

namespace Tools.Logging;
public static class LoggingInstaller {
    
    /// <summary>
    /// Serilog include elasticsearch
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddSerilogLogging(this IServiceCollection services, IConfiguration configuration)
    {
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