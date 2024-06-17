namespace DAL.Entites;

public class Order
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public List<OrderProduct> OrderProducts { get; set; }
}