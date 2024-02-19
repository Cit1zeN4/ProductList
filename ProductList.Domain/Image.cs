namespace ProductList.Domain;

public class Image
{
    public Guid Id { get; set; }
    public File File { get; set; }
    public virtual Product Product { get; set; }
}