using MassTransit;
using ReceiverApplication.Entities;

namespace ReceiverApplication.Consumers;

public class Consumer : IConsumer<Product>
{
    public async Task Consume(ConsumeContext<Product> context)
    {
        var product = context.Message;
        Console.WriteLine("adssds");
    }
}