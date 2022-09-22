using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Producer.Entities;
using Producer.MiddleWares;
using Shared.Entities;

namespace Producer.Controllers;

[ApiController]
[ErrorHandlingFilter]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IPublishEndpoint _publishEndpoint;

    public OrdersController(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateOrder(OrderDto orderDto)
    {
        await _publishEndpoint.Publish<OrderCreated>(new
            {
                id = 1,
                orderDto.ProductName,
                orderDto.Quantity,
                orderDto.Price
            }
        );
        return Ok("Order Created");
    }
}