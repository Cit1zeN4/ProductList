using Microsoft.EntityFrameworkCore;
using ProductList.Application.Interfaces;
using ProductList.Domain;
using File = ProductList.Domain.File;

namespace ProductList.Data;

public class ProductListDbContext : DbContext, IProductListDbContext
{
    public ProductListDbContext(DbContextOptions<ProductListDbContext> options)
        : base(options) { }
    
    public DbSet<File> Files { get; set; }
    public DbSet<UserFile> UserFiles { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCart> ProductCarts { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<Shop> Shops { get; set; }
}