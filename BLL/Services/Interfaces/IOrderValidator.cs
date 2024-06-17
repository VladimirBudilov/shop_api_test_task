using DAL;
using DAL.Entites;

namespace BLL.Services.Interfaces;

public interface IOrderValidator
{
    Task<bool> GetValidProducts(ShopDbContext context, List<OrderProduct>? orderProducts);
    Task<bool> TryGetOrder(Guid id, ShopDbContext context, out Order? o);
}