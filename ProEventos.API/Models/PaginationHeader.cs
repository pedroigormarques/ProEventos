namespace ProEventos.API.Models;
public class PaginationHeader
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public int PageSize { get; set; }

    public PaginationHeader(int currentPage, int pageSize, int totalCount, int totalPages)
    {
        CurrentPage = currentPage;
        TotalPages = totalPages;
        TotalCount = totalCount;
        PageSize = pageSize;
    }
}
