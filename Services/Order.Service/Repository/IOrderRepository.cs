using Orders.Service.Models;

namespace Orders.Service.Repository;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllOrder(int pageNumber,int pageSize);
    Task<Order> GetOrderById(int orderId);
    Task<IEnumerable<OrderDetail>> GetOrderDetails(int orderId);
    Task<IEnumerable<OrderDetail>> GetOrdersDetails(List<int> orderIds);
    Task<bool> UpdateOrder(Order input);
    Task<bool> UpdateOrderDetails(List<OrderDetail> input);
}
