using MediatR;
using Produts.Services.Commands;
using Produts.Services.Models.Dtos;
using Produts.Services.Repository;

namespace Produts.Services.Handlers;

public class GetAllProductHandler : IRequestHandler<AddProductCommad, ResponseDto>
{
    private readonly IProductRepository Repository;

    public GetAllProductHandler(IProductRepository repository)
    {
        Repository = repository;
    }

    async Task<ResponseDto> IRequestHandler<AddProductCommad, ResponseDto>.Handle(AddProductCommad request, CancellationToken cancellationToken)
    {
        try
        {
            var data = await Repository.AddProductAsync(request.Product.GetProdut());
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