using DAL;
using DAL.Entites;

namespace BLL.Validators;

public interface IProductValidator
{
    Task<bool> TryGetOldProduct(Product product, ShopDbContext context, out Product? output);
    Task<bool> TryGetOldProduct(Guid id, ShopDbContext context, out Product? output);
}