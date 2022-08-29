using Common;
using MassTransit;

namespace ReceiverApplication.Consumers;

public class ReqResponse : IConsumer<BalanceUpdate>
{
    public async Task Consume(ConsumeContext<BalanceUpdate> context)
    {
        var data = context.Message;
        var currentBalance = new Balance
        {
            CurrentBalance = 1000
        };
        await context.RespondAsync<Balance>(currentBalance);
    }
}