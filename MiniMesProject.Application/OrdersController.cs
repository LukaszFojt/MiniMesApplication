using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;

    public OrdersController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
    {
        var orders = await _orderRepository.GetAllOrders();
        return Ok(orders);
    }

    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> AddOrder([FromBody] Order order)
    {
        await _orderRepository.AddOrder(order);
        return Ok("Order with ID " + order.Id + " has been added.");
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> DeleteOrder([FromRoute] int id)
    {
        await _orderRepository.DeleteOrder(id);
        return Ok("Order with ID " + id + " has been deleted.");
    }

    [HttpPut]
    [Route("Update/{id}")]
    public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order updatedOrderData)
    {
        await _orderRepository.UpdateOrder(id, updatedOrderData);
        return Ok("Order with ID " + id + " has been updated.");
    }
}
