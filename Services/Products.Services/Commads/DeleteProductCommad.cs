using MediatR;
using Produts.Services.Models.Dtos;

namespace Produts.Services.Commands;

public class DeleteProductCommad : IRequest<ResponseDto>
{
    public DeleteProductCommad(int product)
    {
        Id = product;
    }

    public int Id { get; }
}