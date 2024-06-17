using BLL.Services.Interfaces;
using BLL.Validators;
using DAL;
using DAL.Entites;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services;

public class ProductService(ShopDbContext context, IProductValidator validator) : IProductService
{
    public async Task<List<Product>> GetProductsAsync()
    {
        return await context.Products.AsNoTracking().ToListAsync();
    }
    
    public async Task<Product?> GetProductAsync(Guid id)
    {
        return await context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    }
    
    public async Task<Product?> AddProductAsync(Product product)
    {
        if(await validator.TryGetOldProduct(product, context, out var oldProduct)) return null;
        
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();
        var newProduct = await context.Products.FirstOrDefaultAsync(p => p.Title == product.Title);
        return newProduct;
    }
    
    public async Task<Product?> DeleteProductAsync(Guid id)
    {
        if( !await validator.TryGetOldProduct(id, context, out var product)) return null;
        
        context.Products.Remove(product);
        await context.SaveChangesAsync();
        return product;
    }
}