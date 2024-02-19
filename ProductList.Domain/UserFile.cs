namespace ProductList.Domain;

public class UserFile
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public virtual File File { get; set; }
}