using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;

public class OrderRepository : IOrderRepository
{
    private readonly MiniMesDbContext _dbContext;

    public OrderRepository(MiniMesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<IOrder>> GetAllOrders()
    {
        var order = await _dbContext.Orders.ToListAsync();
        return order.Cast<IOrder>().ToList();
    }

    public async Task AddOrder(IOrder order)
    {
        var concreteOrder = new Order
        {
            Id = order.Id,
            Code = order.Code,
            Quantity = order.Quantity,
            MachineId = order.MachineId,
            ProductId = order.ProductId,
        };

        _dbContext.Orders.Add(concreteOrder);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteOrder(int id)
    {
        var order = await _dbContext.Orders.FindAsync(id);
        if (order != null)
        {
            _dbContext.Orders.Remove(order);
        }       
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateOrder(int id, IOrder updatedOrderData)
    {
        var order = await _dbContext.Orders.FindAsync(id);

        if (order == null)
        {
            throw new ArgumentException("Order not found");
        }

        order.Code = updatedOrderData.Code;
        order.Quantity = updatedOrderData.Quantity;

        order.MachineId = updatedOrderData.MachineId;
        order.ProductId = updatedOrderData.ProductId;

        await _dbContext.SaveChangesAsync();
    }
}

