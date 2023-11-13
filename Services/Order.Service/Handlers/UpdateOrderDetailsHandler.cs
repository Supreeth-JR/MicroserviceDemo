using MassTransit;
using MediatR;
using Orders.Service.Commands;
using Orders.Service.Models;
using Orders.Service.Models.Dtos;
using Orders.Service.Repository;

namespace Orders.Service.Handlers;

public class UpdateOrderDetailsHandler : IRequestHandler<UpdateOrderCommand, ResponseDto>
{
    private readonly IOrderRepository Repository;
    private IPublishEndpoint _PublishEndPoint;

    public UpdateOrderDetailsHandler(IOrderRepository repository, IPublishEndpoint publishEndpoint)
    {
        Repository = repository;
        _PublishEndPoint = publishEndpoint;
    }

    public async Task<ResponseDto> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
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

            var orderDetails = request.Order.OrderDetails.Select(x => new OrderDetail
            {
                Discount = x.Discount,
                OrderId = x.Id,
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice
            }).ToList();

            var orderResult = await Repository.UpdateOrder(order);
            var orderDetailsResult = await Repository.UpdateOrderDetails(orderDetails);

            var orderDetailsToPublish = orderDetails.Select(x => new OrderPublishDto
            {
                ProductId = x.ProductId,
                ProductQty = x.Quantity
            }).ToList();
            var publisjResultawait = _PublishEndPoint.Publish(orderDetailsToPublish);

            return new ResponseDto
            {
                IsSuccess = true,
                Response = request.Order,
                ResponseMessage = orderDetailsResult && orderResult 
                    ? "Order details updated." 
                    : "Order details update failed."
            };
        }
        catch (Exception ex)
        {
            return new ResponseDto
            {
                IsSuccess = true,
                ResponseMessage = ex.Message
            };
        }
    }
}
