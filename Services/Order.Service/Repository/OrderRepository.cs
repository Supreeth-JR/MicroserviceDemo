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

    public async Task<IEnumerable<OrderDetail>> GetOrdersDetails(List<int> orderIds)
    {
        return await Context.OrderDetails.Where(order => orderIds.Any(o => o == order.OrderId)).ToListAsync();
    }

    public async Task<Order> GetOrderById(int orderId)
    {
        return await Context.Orders.Where(order=> order.OrderId == orderId).FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateOrder(Order input)
    {
        await Context.Database.BeginTransactionAsync();
        try
        {
            Context.Orders.Update(input);
            await Context.Database.CommitTransactionAsync();
            await Context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            await Context.Database.RollbackTransactionAsync();
            return false;
        }
    }

    public async Task<bool> UpdateOrderDetails(List<OrderDetail> input)
    {
        await Context.Database.BeginTransactionAsync();
        try
        {
            Context.OrderDetails.UpdateRange(input);
            await Context.Database.CommitTransactionAsync();
            await Context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            await Context.Database.RollbackTransactionAsync();
            return false;
        }
    }
}
