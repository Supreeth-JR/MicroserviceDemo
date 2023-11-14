using MediatR;
using Orders.Service.Models.Dtos;

namespace Orders.Service.Commands;

public class CreateOrderCommand : IRequest<ResponseDto>
{
    public CreateOrderCommand(OrderDto order)
    {
        Order = order;
    }

    public OrderDto Order { get; }
}
