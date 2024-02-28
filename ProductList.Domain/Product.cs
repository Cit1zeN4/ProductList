namespace ProductList.Domain;

public class Product
{
    public Guid Id { get; set; }
    public string Barcode { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public virtual List<Image> Images { get; set; }
    public virtual List<Shop> Shops { get; set; }
    public virtual List<Purchase> Carts { get; set; } 
}