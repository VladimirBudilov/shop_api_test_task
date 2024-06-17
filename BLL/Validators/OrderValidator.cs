using BLL.Services.Interfaces;
using DAL;
using DAL.Entites;
using Microsoft.EntityFrameworkCore;

namespace BLL.Validators;

public class OrderValidator: IOrderValidator
{
    public Task<bool> GetValidProducts(ShopDbContext context, List<OrderProduct>? orderProducts)
    {
        foreach (var product in orderProducts)
        {
            var oldProduct = context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == product.ProductId).Result;
            if (oldProduct == null) return Task.FromResult(false);
        }
        return Task.FromResult(true);
    }

    public Task<bool> TryGetOrder(Guid id, ShopDbContext context, out Order? o)
    {
        o = context.Orders
            .AsNoTracking()
            .Include(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .FirstOrDefaultAsync(o => o.Id == id).Result;
        return Task.FromResult(o != null);
    }
}