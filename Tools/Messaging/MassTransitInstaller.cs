using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Tools.Messaging.Consumers;
using Tools.Messaging.Messages;

namespace Tools.Messaging;
public static class MassTransitInstaller
{
    public static IServiceCollection AddAsyncProcessing(this IServiceCollection services, IConfiguration configuration, Assembly[] assembliesWithConsumers)
    {
        var configData = configuration.GetSection("RabbitMQSettings");
        var brokerSettings = new BrokerSettings();
        configData.Bind(brokerSettings);

        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            x.SetInMemorySagaRepositoryProvider();

            var entryAssembly = Assembly.GetEntryAssembly();

            x.AddConsumer<MyConsumer>();
            x.AddSagaStateMachines(entryAssembly);
            x.AddSagas(entryAssembly);
            x.AddActivities(entryAssembly);

            x.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });

        services.AddSingleton<ILockStatementProvider, PostgresLockStatementProvider>();
        services.AddTransient<IMessageSender, MessageSender>();

        services.AddHostedService<Worker>();

        return services;
    }

    public static IApplicationBuilder ApplyOutboxMigrations(this IApplicationBuilder app)
    {
        return app;
    }
}

public class Worker(IMessageSender sender) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await sender
                .PublishMessageAsync(new HelloMessage("RabbitMQ"), stoppingToken)
                .ConfigureAwait(false);

            await Task.Delay(1000, stoppingToken).ConfigureAwait(false);
        }
    }
}

