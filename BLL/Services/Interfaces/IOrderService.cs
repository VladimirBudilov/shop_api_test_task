using DAL.Entites;

namespace BLL.Services.Interfaces;

public interface IOrderService
{
    Task<List<Order>> GetOrdersAsync();
    Task<Order?> CreateOrderAsync(Order order);
    Task<Order?> DeleteOrderAsync(Guid id);
    Task<Order?> GetOrderAsync(Guid id);
}