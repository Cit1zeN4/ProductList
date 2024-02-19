using Microsoft.EntityFrameworkCore;
using ProductList.Domain;
using File = ProductList.Domain.File;

namespace ProductList.Application.Interfaces;

public interface IProductListDbContext
{
    DbSet<File> Files { get; set; }
    DbSet<UserFile> UserFiles { get; set; }
    DbSet<Image> Images { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<ProductCart> ProductCarts { get; set; }
    DbSet<Purchase> Purchases { get; set; }
    DbSet<Shop> Shops { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}