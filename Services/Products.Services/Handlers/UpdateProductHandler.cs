using MediatR;
using Produts.Services.Commands;
using Produts.Services.Models.Dtos;
using Produts.Services.Repository;

namespace Produts.Services.Handlers;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ResponseDto>
{
    private readonly IProductRepository Repository;

    public UpdateProductHandler(IProductRepository repository)
    {
        Repository = repository;
    }

    async Task<ResponseDto> IRequestHandler<UpdateProductCommand, ResponseDto>.Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var data = await Repository.UpdateProductAsync(request.Product.GetProdut());
            request.Product.Id = data;
            return new ResponseDto()
            {
                IsSuccess = data != 0,
                Response = request.Product
            };
        }
        catch (Exception exception)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                ResponseMessage = exception.Message
            };
        }
    }
}