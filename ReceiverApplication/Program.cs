using System.Reflection;
using MassTransit;
using ReceiverApplication.Consumers;

namespace ReceiverApplication;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddMassTransit(x =>
        {
            x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("admin");
                    h.Password("admin");
                });
                cfg.ReceiveEndpoint("produce", ep => ep.Consumer<Consumer>());
                cfg.ReceiveEndpoint("publish", ep => ep.Consumer<Publisher>());
                cfg.ReceiveEndpoint("send-get", ep => ep.Consumer<ReqResponse>());
            }));
            x.AddConsumer<Consumer>();
            x.AddConsumer<Publisher>();
            x.AddConsumer<ReqResponse>();
        });
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}