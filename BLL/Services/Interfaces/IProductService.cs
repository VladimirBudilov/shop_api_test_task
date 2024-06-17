using DAL.Entites;

namespace BLL.Services.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetProductsAsync();
    Task<Product?> AddProductAsync(Product product);
    Task<Product?> DeleteProductAsync(Guid id);
    Task<Product?> GetProductAsync(Guid id);
}