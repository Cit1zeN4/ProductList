namespace ProductList.Application.Common;

public class DataList<T>
{
    public int TotalCount { get; set; }
    public List<T> Records { get; set; } = new();
}