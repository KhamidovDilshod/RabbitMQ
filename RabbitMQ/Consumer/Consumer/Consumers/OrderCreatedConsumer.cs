using MassTransit;
using Newtonsoft.Json;
using Shared.Entities;

namespace Consumer.Consumers;

public class OrderCreatedConsumer:IConsumer<OrderCreated>
{
    public async Task Consume(ConsumeContext<OrderCreated> context)
    {
        var jsonMessage = JsonConvert.SerializeObject(context.Message);
        Console.WriteLine($"Order created :{jsonMessage}");
    }
}