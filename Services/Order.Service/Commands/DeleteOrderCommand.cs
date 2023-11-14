using MediatR;
using Orders.Service.Models.Dtos;

namespace Orders.Service.Commands;

public class DeleteOrderCommand : IRequest<ResponseDto>
{
    public DeleteOrderCommand(int id)
    {
        Id = id;
    }

    public int Id { get; }
}
