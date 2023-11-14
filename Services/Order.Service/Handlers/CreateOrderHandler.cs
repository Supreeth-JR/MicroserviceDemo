using MediatR;
using Orders.Service.Commands;
using Orders.Service.Models;
using Orders.Service.Models.Dtos;
using Orders.Service.Repository;

namespace Orders.Service.Handlers;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, ResponseDto>
{
    private readonly IOrderRepository Repository;

    public CreateOrderHandler(IOrderRepository repository)
    {
        Repository = repository;
    }

    public async Task<ResponseDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var order = new Order
            {
                CustomerId = request.Order.CustomerId,
                OrderDate = request.Order.OrderDate,
                EmployeeId = request.Order.EmployeeId,
                OrderId = request.Order.Id,
                ShipName = request.Order.ShipmentDetails.Name,
                ShippedDate = request.Order.ShippedDate,
                RequiredDate = request.Order.RequiredDate,
                ShipVia = request.Order.ShipmentDetails.Via,
                ShipAddress = request.Order.ShipmentDetails.Address,
                ShipCity = request.Order.ShipmentDetails.City,
                ShipRegion = request.Order.ShipmentDetails.Region,
                ShipPostalCode = request.Order.ShipmentDetails.PostalCode,
                ShipCountry = request.Order.ShipmentDetails.Country,
            };

            var orderDetails = request.Order.OrderDetails?.Select(x => new OrderDetail
            {
                Discount = x.Discount,
                OrderId = x.Id,
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice
            }).ToList();

            var isOrderCreated = await Repository.CreateOrder(order, orderDetails);
            return new ResponseDto
            {
                IsSuccess = isOrderCreated,
                ResponseMessage = "Order created succesfully."
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
