using MediatR;
using Microsoft.AspNetCore.Mvc;
using Orders.Service.Commands;
using Orders.Service.Models.Dtos;
using Orders.Service.Queries;

namespace Orders.Service.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMediator Mediator;

    public OrderController(IMediator mediator)
    {
        Mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrders([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        return Ok(await Mediator.Send(new GetAllOrdersQuery(pageSize, pageNumber)));
    }

    [HttpGet("{orderId:int}")]
    public async Task<IActionResult> GetOrderById(int orderId)
    {
        return Ok(await Mediator.Send(new GetOrderById(orderId)));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateOrder(OrderDto order)
    {
        return Ok(await Mediator.Send(new UpdateOrderCommand(order)));
    }
}
