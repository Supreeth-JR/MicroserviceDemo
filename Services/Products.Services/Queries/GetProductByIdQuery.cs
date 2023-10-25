using MediatR;
using Produts.Services.Models.Dtos;

namespace Produts.Services.Queries;

public class GetProductByIdQuery : IRequest<ResponseDto>
{
    public GetProductByIdQuery(int id)
    {
        Id = id;
    }

    public int Id { get; }
}