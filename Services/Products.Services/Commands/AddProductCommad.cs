using MediatR;
using Produts.Services.Models.Dtos;

namespace Produts.Services.Commands;

public class AddProductCommad : IRequest<ResponseDto>
{
    public AddProductCommad(ProductDto product)
    {
        Product = product;
    }

    public ProductDto Product { get; }
}