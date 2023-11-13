using MediatR;
using Orders.Service.Models.Dtos;

namespace Orders.Service.Commands;

public class UpdateOrderCommand : IRequest<ResponseDto>
{
    public UpdateOrderCommand(OrderDto order)
    {
        Order = order;
    }

    public OrderDto Order { get;}
}
