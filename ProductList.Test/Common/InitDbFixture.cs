using AutoMapper;
using ProductList.Application.Common.Mappings;
using ProductList.Application.Interfaces;
using ProductList.Data;

namespace ProductList.Test.Common;

public class InitDbFixture : IDisposable
{
    public ProductListDbContext Context { get; private set; }
    public IMapper Mapper { get; private set; }

    public InitDbFixture()
    {
        Context = TestDbContextFactory.Create();
        
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AssemblyMappingProfile(
                typeof(IProductListDbContext).Assembly));
        });
        Mapper = configurationProvider.CreateMapper();
    }

    public void Dispose()
    {
        Context.Dispose();
    }
    
    [CollectionDefinition("InitDbCollection")]
    public class InitDbCollection : ICollectionFixture<InitDbFixture> { }
}