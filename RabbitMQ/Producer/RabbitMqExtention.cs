using System.Net;
using System.Reflection;
using MassTransit;
using Sender.Entities;

namespace Sender;

public static class RabbitMqExtention
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            x.SetInMemorySagaRepositoryProvider();
            var assembly = Assembly.GetEntryAssembly();
            x.AddConsumers(assembly);
            x.AddSagaStateMachines(assembly);
            x.AddSagas(assembly);
            x.AddActivities(assembly);
    
            x.AddRequestClient<OrderDto>();
    
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("admin");
                    h.Password("admin");
                });
                cfg.ConfigureEndpoints(context);
            });
        });
        return services;
    }
}