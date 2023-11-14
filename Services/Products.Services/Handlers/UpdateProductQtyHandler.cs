using MediatR;
using Products.Services.Commands;
using Produts.Services.Repository;

namespace Products.Services.Handlers;

public class UpdateProductQtyHandler : IRequestHandler<UpdateProductQtyCommand>
{
    private readonly IProductRepository Repository;

    public UpdateProductQtyHandler(IProductRepository repository)
    {
        Repository = repository;
    }

    public async Task Handle(UpdateProductQtyCommand request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            return;
        }

        foreach (var item in request.Products)
        {
            var product = await Repository.GetProductByIdAsync(item.ProductId);
            product.UnitsInStock -= (int)item.ProductQty;
            product.UnitsOnOrder += (int)item.ProductQty;

            await Repository.UpdateProductAsync(product);
        }
    }
}
