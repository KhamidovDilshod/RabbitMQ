using Common;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using ReceiverApplication.Entities;

namespace SenderApplication.Controllers;

[ApiController]
[Route("api")]
public class SenderController : ControllerBase
{
    private readonly IBus _bus;
    private readonly IRequestClient<FileProcess> _client;

    public SenderController(IBus bus, IRequestClient<FileProcess> client)
    {
        _bus = bus;
        _client = client;
    }

    [HttpPost("produce")]
    public async Task<ActionResult> Sender()
    {
        var product = new Product
        {
            Name = "Victus",
            Price = 1000,
            Message = "asd"
        };
        var url = new Uri("rabbitmq://localhost/produce");
        var endpoint = await _bus.GetSendEndpoint(url);
        await endpoint.Send(product);
        Console.WriteLine("Sent");
        return Ok(product);
    }

    [HttpPost("publish")]
    public async Task<IActionResult> Publisher()
    {
        var person = new Person()
        {
            Name = "Dilshodbek",
            Email = "khamidovdilshodbek@gmail.com"
        };
        await _bus.Publish(person);
        return Ok(person);
    }

    [HttpPost("send-get")]
    public async Task<IActionResult> PublisherAndGetResponse()
    {
        var balanceRequest = new BalanceUpdate()
        {
            TypeOfInstruction = '-',
            Amount = 100
        };
        var request = _client.Create(balanceRequest);
        var response = await request.GetResponse<Balance>();
        return Ok(response);
    }

    [HttpGet("file")]
    public async Task<IActionResult> FileProcess()
    {
        var process = new FileProcess()
        {
            FileName = "null",
            FilePath = "path",
            IsSuccess = false
        };
        var request = _client.Create(process);
        var response = await request.GetResponse<FileUpdate>();
        return Ok(response);
    }
}