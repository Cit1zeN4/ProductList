namespace ProductList.Domain;

public class ProductCart
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public Guid UserId { get; set; }
    public double Price { get; set; }
    public virtual Product Product { get; set; }
    public virtual Shop Shop { get; set; }
    public virtual Purchase Cart { get; set; }
}