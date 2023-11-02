using Orders.Service.Models;

namespace Orders.Service.Repository;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllOrder(int pageNumber,int pageSize);
    Task<IEnumerable<OrderDetail>> GetOrderDetails(int orderId);
}
