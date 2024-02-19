using Microsoft.EntityFrameworkCore;
using ProductList.Domain;
using File = ProductList.Domain.File;

namespace ProductList.Application.Interfaces;

public interface IProductListDbContext
{
    DbSet<File> Files { get; set; }
    DbSet<UserFile> UserFiles { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<ProductCart> ProductCarts { get; set; }
    DbSet<Purchase> Purchase { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}