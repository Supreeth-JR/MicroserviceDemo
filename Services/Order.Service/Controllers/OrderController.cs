using MediatR;
using Microsoft.AspNetCore.Mvc;
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

    [HttpGet("{pageNumber:int},{pageSize:int}")]
    public async Task<IActionResult> GetAllOrders(int pageNumber, int pageSize)
    {
        return Ok(await Mediator.Send(new GetAllOrdersQuery(pageSize, pageNumber)));
    }
}
