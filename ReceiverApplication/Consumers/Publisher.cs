using Common;
using MassTransit;

namespace ReceiverApplication.Consumers;

public class Publisher : IConsumer<Person>
{
    public async Task Consume(ConsumeContext<Person> context)
    {
        var info = context.Message;
        Console.WriteLine("Publisher Received");
    }
}