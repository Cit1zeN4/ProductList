namespace ProductList.Application.Common;

public sealed class DataList<T>
{
    public int TotalCount { get; set; }
    public List<T> Records { get; set; } = new();
}