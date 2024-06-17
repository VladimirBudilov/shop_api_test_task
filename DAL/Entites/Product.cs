namespace DAL.Entites;

public class Product
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    
    public List<OrderProduct> OrderProducts { get; set; }
}