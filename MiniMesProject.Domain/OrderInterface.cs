public interface IOrder
{
    int Id { get; set; }
    string Code { get; set; }
    int Quantity { get; set; }
    int MachineId { get; set; }
    int ProductId { get; set; }
}

public interface IOrderRepository
{
    Task<List<IOrder>> GetAllOrders();
    Task AddOrder(IOrder order);
    Task DeleteOrder(int id);
    Task UpdateOrder(int id, IOrder updatedOrderData);
}