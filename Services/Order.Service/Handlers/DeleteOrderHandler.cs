using MediatR;
using Orders.Service.Commands;
using Orders.Service.Models.Dtos;
using Orders.Service.Repository;

namespace Orders.Service.Handlers;

public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, ResponseDto>
{
    private readonly IOrderRepository Repository;

    public DeleteOrderHandler(IOrderRepository repository)
    {
        Repository = repository;
    }
    public async Task<ResponseDto> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await Repository.GetOrderById(request.Id);
            if (order is null)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    ResponseMessage = "Order not found."
                };
            }

            var orderDetails = await Repository.GetOrderDetails(request.Id);
            bool isDeleted = await Repository.DeleteOrder(order, orderDetails);

            return new ResponseDto
            {
                IsSuccess = isDeleted,
            };
        }
        catch (Exception ex)
        {
            return new ResponseDto
            {
                IsSuccess = false,
                ResponseMessage = ex.Message
            };
        }
    }
}
