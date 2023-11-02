using Microsoft.EntityFrameworkCore;
using Orders.Service.Models;

namespace Orders.Service.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly NorthwindContext Context;

    public OrderRepository(NorthwindContext context)
    {
        Context = context;
    }

    public async Task<IEnumerable<Order>> GetAllOrder(int pageNumber, int pageSize)
    {
        return await Context.Orders.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();
    }

    public async Task<IEnumerable<OrderDetail>> GetOrderDetails(int orderId)
    {
        return await Context.OrderDetails.Where(order => order.OrderId == orderId).ToListAsync();
    }
}
