namespace MindLog.SharedKernel.Pagination;

public class PaginationParams
{
    private const int DefaultPageNumber = 1;
    private const int DefaultPageSize = 20;
    private const int MinPageNumber = 1;
    private const int MinPageSize = 10;
    private const int MaxPageSize = 100;

    public int PageNumber { get; set; } = DefaultPageNumber;

    public int PageSize { get; set; } = DefaultPageSize;

    public void Normalize()
    {
        if (PageNumber < MinPageNumber)
            PageNumber = DefaultPageNumber;

        if (PageSize < MinPageSize)
            PageSize = MinPageSize;

        else if (PageSize > MaxPageSize)
            PageSize = MaxPageSize;
    }
}
