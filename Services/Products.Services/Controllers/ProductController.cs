using MediatR;
using Microsoft.AspNetCore.Mvc;
using Produts.Services.Commands;
using Produts.Services.Models.Dtos;
using Produts.Services.Queries;

namespace Produts.Services.Controllers;

[ApiController]
[Route("api/product")]
public class ProductContoller : ControllerBase
{
    private readonly IMediator Mediator;

    public ProductContoller(IMediator mediator)
    {
        Mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var result = await Mediator.Send(new GetAllProductQuery());
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var result = await Mediator.Send(new GetProductByIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Product(ProductDto product)
    {
        var result = await Mediator.Send(new AddProductCommad(product));
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct(ProductDto product)
    {
        var result = await Mediator.Send(new UpdateProductCommand(product));
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var result = await Mediator.Send(new DeleteProductCommad(id));
        return Ok(result);
    }
}