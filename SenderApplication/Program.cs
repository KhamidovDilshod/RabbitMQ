using System.Reflection;
using Common;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();
    x.SetInMemorySagaRepositoryProvider();
    var assembly = Assembly.GetEntryAssembly();
    x.AddConsumers(assembly);
    x.AddSagaStateMachines(assembly);
    x.AddSagas(assembly);
    x.AddActivities(assembly);
    x.AddRequestClient<BalanceUpdate>();
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
// builder.Services.AddMassTransitHostedService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();