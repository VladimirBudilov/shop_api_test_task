using DAL;
using DAL.Entites;
using Microsoft.EntityFrameworkCore;

namespace BLL.Validators;

public class ProductValidator : IProductValidator
{
    public Task<bool> TryGetOldProduct(Product product, ShopDbContext context, out Product? output)
    {
        output =  context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Title == product.Title).Result;
        return Task.FromResult(output != null);
    }
    
    public Task<bool> TryGetOldProduct(Guid id, ShopDbContext context, out Product? output)
    {
        output =  context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id).Result;
        return Task.FromResult(output != null);
    }
}