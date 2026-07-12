namespace E_Commerce.Application.Common;

public class ProductQueryParams
{
    public int? BrandId { get; set; }
    public int? TypeId { get; set; }
    public string? SearchValue { get; set; }
    public ProductSortingOptions Sort { get; set; }

    public int PageIndex { get; set; } = 1;

    private const int _defaultPageSize = 5;
    private const int _maxPageSize = 10;

    private int _pageSize = _defaultPageSize;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > _maxPageSize ? _maxPageSize : (value < 1 ? _defaultPageSize: value);
    }
}