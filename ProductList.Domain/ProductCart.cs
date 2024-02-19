namespace ProductList.Domain;

public class ProductCart
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public double Price { get; set; }
    public virtual Product Product { get; set; }
    public virtual Shop Shop { get; set; }
    public virtual Сart Cart { get; set; }
    public virtual User User { get; set; }
}