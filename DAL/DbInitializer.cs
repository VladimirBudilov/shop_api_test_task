using DAL;
using DAL.Entites;
using System;
using System.Collections.Generic;

namespace DAL;

public static class DbInitializer
{
 public static void Initialize(ShopDbContext context)
{
    context.Database.EnsureCreated();

    if (context.Orders.Any())
    {
        return;
    }

    var products = new List<Product>
    {
        new Product { Id = Guid.NewGuid(), Title = "Pryanik", Price = 10 },
        new Product { Id = Guid.NewGuid(), Title = "PryanikFromTula", Price = 15 }
    };

    foreach (var product in products)
    {
        context.Products.Add(product);
    }

    var orders = new List<Order>
    {
        new Order { Id = Guid.NewGuid(), CreatedAt = DateTime.Now },
        new Order { Id = Guid.NewGuid(), CreatedAt = DateTime.Now }
    };

    foreach (var order in orders)
    {
        foreach (var product in products)
        {
            var orderProduct = new OrderProduct
            {
                OrderId = order.Id,
                ProductId = product.Id,
                Quantity = new Random().Next(1, 10)
            };
            context.OrderProducts.Add(orderProduct);
        }

        context.Orders.Add(order);
    }

    context.SaveChanges();
}
}