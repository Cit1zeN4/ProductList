namespace ProductList.Domain;

public class Shop
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public DateTimeOffset CreateAt { get; set; } = DateTimeOffset.UtcNow;
}