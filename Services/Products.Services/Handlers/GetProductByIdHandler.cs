using MediatR;
using Produts.Services.Models.Dtos;
using Produts.Services.Queries;
using Produts.Services.Repository;

namespace Produts.Services.Handlers;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ResponseDto>
{
    private readonly IProductRepository Repository;

    public GetProductByIdHandler(IProductRepository repository)
    {
        Repository = repository;
    }

    async Task<ResponseDto> IRequestHandler<GetProductByIdQuery, ResponseDto>.Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var data = await Repository.GetProductByIdAsync(request.Id);
            return new ResponseDto()
            {
                IsSuccess = data is not null,
                Response = data?.GetProductDto(),
                ResponseMessage = data is not null ? ""  : "Product not found"
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