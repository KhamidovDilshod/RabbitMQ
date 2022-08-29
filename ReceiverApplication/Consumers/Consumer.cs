using MassTransit;
using ReceiverApplication.Entities;

namespace ReceiverApplication;

public class Consumer : IConsumer<Product>
{
    public async Task Consume(ConsumeContext<Product> context)
    {
        var product = context.Message;
        Console.WriteLine("adssds");
        // await context.Publish<string>(new {context.Message});
    }
}