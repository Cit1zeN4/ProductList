using ProductList.Data;

namespace ProductList.Test.Common;

public abstract class TestCommandBase : IDisposable
{
    protected readonly ProductListDbContext Context = ProductListContextFactory.Create();

    public void Dispose()
    {
        ProductListContextFactory.Destroy(Context);
    }
}