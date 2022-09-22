using Consumer.Consumers;
using MassTransit;

namespace Consumer;

public static class RabbitMqExtension
{
    public static IServiceCollection AddConsumer(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.ReceiveEndpoint("order-created", e => { e.Consumer<OrderCreatedConsumer>(); });

                cfg.Host("localhost", "/", h =>
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