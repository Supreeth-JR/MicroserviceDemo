using MediatR;
using Orders.Service.Queries;
using Orders.Service.Models.Dtos;
using Orders.Service.Repository;

namespace Orders.Service.Handlers;

public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, ResponseDto>
{
    private readonly IOrderRepository Repository;

    public GetAllOrdersHandler(IOrderRepository repository)
    {
        Repository = repository;
    }

    public async Task<ResponseDto> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        ResponseDto response = null;
        try
        {
            var orders = await Repository.GetAllOrder(request.PageNumber,request.PageSize);
            var orderIds = orders.AsParallel().Select(x => x.OrderId).ToList();
            var ordersDetails = await Repository.GetOrdersDetails(orderIds);

            var orderDto = orders.AsParallel().Select(order => new OrderDto
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
                OrderDetails = ordersDetails
                    .Where(od => od.OrderId == order.OrderId)
                    .Select(order => new OrderDetailsDto
                    {
                        Discount = order.Discount,
                        Id = order.OrderId,
                        ProductId = order.ProductId,
                        Quantity = order.Quantity,
                        UnitPrice = order.UnitPrice,
                    }).ToList(),
            });

            return new ResponseDto
            {
                IsSuccess = true,
                Response = orderDto
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
