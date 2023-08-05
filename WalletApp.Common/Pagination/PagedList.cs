namespace WalletApp.Common.Pagination;

public class PagedList<T>
{
    private readonly PageParameters _pageParameters;

    public int PageNumber => _pageParameters.PageNumber;
    public int PageSize => _pageParameters.PageSize;
    public int TotalCount { get; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

    public IEnumerable<T> Items { get; }

    public PagedList(IEnumerable<T> items, int count, PageParameters pageParameters)
    {
        _pageParameters = pageParameters;

        TotalCount = count;

        Items = items;
    }

    public PagedList<TOther> Create<TOther>(IEnumerable<TOther> others)
    {
        return new PagedList<TOther>(others, TotalCount, _pageParameters);
    }
}
