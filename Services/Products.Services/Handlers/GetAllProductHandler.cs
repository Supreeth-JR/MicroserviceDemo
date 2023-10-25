using MediatR;
using Produts.Services.Models.Dtos;
using Produts.Services.Queries;
using Produts.Services.Repository;

namespace Produts.Services.Handlers;

public class AddProductHandler : IRequestHandler<GetAllProductQuery, ResponseDto>
{
    private readonly IProductRepository Repository;

    public AddProductHandler(IProductRepository repository)
    {
        Repository = repository;
    }

    async Task<ResponseDto> IRequestHandler<GetAllProductQuery, ResponseDto>.Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var data = await Repository.GetAllProductsAsync();
            return new ResponseDto()
            {
                IsSuccess = data.Count() > 0,
                Response = data.Select(x => x.GetProductDto()).ToList()
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