using Contrcts;
using MediatR;
using Produts.Services.Models.Dtos;

namespace Products.Services.Commands;

public class UpdateProductQtyCommand : IRequest
{
    public UpdateProductQtyCommand(List<ProductToBeUpdated> products)
    {
        this.Products = products;
    }

    public List<ProductToBeUpdated> Products { get; }
}
