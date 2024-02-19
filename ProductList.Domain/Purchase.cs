namespace ProductList.Domain;

public class Purchase
{
    public Guid Id { get; set; }
    public DateTimeOffset Date { get; set; }
    public Guid UserId { get; set; }
    public virtual List<ProductCart> Products { get; set; }
}