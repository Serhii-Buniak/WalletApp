namespace WalletApp.Common.Pagination;

public class PagedList<T> : List<T>
{
    private readonly PageParameters _pageParameters;

    public int CurrentPage { get; }
    public int PageSize { get; }
    public int TotalCount { get; }

    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    public PagedList(List<T> items, int count, PageParameters pageParameters)
    {
        _pageParameters = pageParameters;
        int pageNumber = pageParameters.PageNumber;
        int pageSize = pageParameters.PageSize;

        TotalCount = count;
        PageSize = pageSize;
        CurrentPage = pageNumber;

        AddRange(items);
    }

    public PagedList<TOther> Create<TOther>(List<TOther> others)
    {
        return new PagedList<TOther>(others, TotalCount, _pageParameters);
    }
}
