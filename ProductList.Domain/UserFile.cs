namespace ProductList.Domain;

public class UserFile
{
    public Guid Id { get; set; }
    public virtual File File { get; set; }
    public virtual User User { get; set; }
}