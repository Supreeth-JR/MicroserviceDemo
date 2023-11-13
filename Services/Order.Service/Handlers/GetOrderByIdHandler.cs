using MediatR;
using Orders.Service.Models;
using Orders.Service.Models.Dtos;
using Orders.Service.Queries;
using Orders.Service.Repository;

namespace Orders.Service.Handlers;

public class GetOrderByIdHandler : IRequestHandler<GetOrderById, ResponseDto>
{
    private readonly IOrderRepository Repository;

    public GetOrderByIdHandler(IOrderRepository repository)
    {
        Repository = repository;
    }

    public async Task<ResponseDto> Handle(GetOrderById request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await Repository.GetOrderById(request.OrderId);
            if (order is null) 
            {
                return new ResponseDto
                {
                    IsSuccess = true,
                    ResponseMessage = "No order details found.",
                };
            }

            var orderDetails = await Repository.GetOrderDetails(request.OrderId);
            if (orderDetails is null)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    ResponseMessage = "No order details found.",
                    Response = GetDtoObject(order)
                };
            }

            return new ResponseDto
            {
                IsSuccess = true,
                Response = GetDtoObject(order, orderDetails)
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

    private OrderDto GetDtoObject(Order order, IEnumerable<OrderDetail> orderDetail = null)
    {
        return new OrderDto
        {
            CustomerId = order.CustomerId,
            EmployeeId = order.EmployeeId,
            Id = order.OrderId,
            OrderDate = order.OrderDate,
            RequiredDate = order.RequiredDate, 
            ShipmentDetails = new ShipmentDetailsDto
            {
                Address = order.ShipAddress,
                City = order.ShipCity,
                Country = order.ShipCountry,
                Freight = order.Freight,
                Name = order.ShipName,
                PostalCode = order.ShipPostalCode,
                Region = order.ShipRegion,
                Via = order.ShipVia,
            },
            ShippedDate = order.ShippedDate,
            OrderDetails = orderDetail?
                    .Where(od => od.OrderId == order.OrderId)
                    .Select(order => new OrderDetailsDto
                    {
                        Discount = order.Discount,
                        Id = order.OrderId,
                        ProductId = order.ProductId,
                        Quantity = order.Quantity,
                        UnitPrice = order.UnitPrice,
                    }).ToList(),
        };
    }
}
