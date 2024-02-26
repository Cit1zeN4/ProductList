using ProductList.Data;

namespace ProductList.Test.Common;

public class InitDbFixture : IDisposable
{
    public ProductListDbContext Context { get; private set; }
    
    public InitDbFixture()
    {
        Context = TestDbContextFactory.Create();
    }
    
    public void Dispose()
    {
        Context.Dispose();
    }
    
    [CollectionDefinition("InitDbCollection")]
    public class InitDbCollection : ICollectionFixture<InitDbFixture> { }
}