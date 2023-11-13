using MediatR;
using Orders.Service.Models.Dtos;

namespace Orders.Service.Queries;

public class GetOrderById : IRequest<ResponseDto>
{
    public GetOrderById(int orderId)
    {
        OrderId = orderId;
    }

    public int OrderId { get;}
}
