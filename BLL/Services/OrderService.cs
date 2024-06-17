using BLL.Services.Interfaces;
using DAL;
using DAL.Entites;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services;

public class OrderService(ShopDbContext context, IOrderValidator validator) : IOrderService
{
    
    public async Task<List<Order>> GetOrdersAsync()
    {
        return await context.Orders.AsNoTracking().Include(o => o.OrderProducts).ThenInclude(op => op.Product).ToListAsync();
    }
    
    public async Task<Order?> CreateOrderAsync(Order order)
    {
        
        if(!await validator.GetValidProducts(context, order.OrderProducts)) return null;
        
        order.CreatedAt = DateTime.Now;
        await context.Orders.AddAsync(order);
        await context.SaveChangesAsync();
        
        order.OrderProducts
            .ForEach(op => op.Product = context.Products
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == op.ProductId));
        return order;
    }
    
    public async Task<Order?> DeleteOrderAsync(Guid id)
    {
        if(!await validator.TryGetOrder(id, context, out var order)) return null;
        context.Orders.Remove(order);
        await context.SaveChangesAsync();
        return order;
    }

    public async Task<Order?> GetOrderAsync(Guid id)
    {
        return await context.Orders.Include(o => o.OrderProducts).ThenInclude(op => op.Product).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }
}