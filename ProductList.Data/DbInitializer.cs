using Microsoft.EntityFrameworkCore;

namespace ProductList.Data;

public class DbInitializer
{
    public static void Initialize(ProductListDbContext context)
    {
        context.Database.Migrate();
    }
}