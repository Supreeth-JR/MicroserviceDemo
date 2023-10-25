using MediatR;
using Produts.Services.Commands;
using Produts.Services.Models.Dtos;
using Produts.Services.Repository;

namespace Produts.Services.Handlers;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommad, ResponseDto>
{
    private readonly IProductRepository Repository;

    public DeleteProductHandler(IProductRepository repository)
    {
        Repository = repository;
    }

    async Task<ResponseDto> IRequestHandler<DeleteProductCommad, ResponseDto>.Handle(DeleteProductCommad request, CancellationToken cancellationToken)
    {
        try
        {
            var data = await Repository.GetProductByIdAsync(request.Id);
            if (data is null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    ResponseMessage = "Product not found"
                };
            }

            await Repository.DeleteProductAsync(request.Id);
            
            return new ResponseDto()
            {
                IsSuccess = true,
                Response = data
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