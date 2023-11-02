using MediatR;
using Produts.Services.Models.Dtos;

namespace Produts.Services.Commands;

public class UpdateProductCommand : IRequest<ResponseDto>
{
    public UpdateProductCommand(ProductDto product)
    {
        Product = product;
    }

    public ProductDto Product { get; }
}