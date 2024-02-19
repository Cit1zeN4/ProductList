namespace ProductList.Domain;

public class Сart
{
    public Guid Id { get; set; }
    public DateTimeOffset Date { get; set; }
    public virtual User User { get; set; }
    public virtual List<ProductCart> Products { get; set; }
}