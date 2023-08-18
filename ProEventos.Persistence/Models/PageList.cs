using Microsoft.EntityFrameworkCore;

namespace ProEventos.Persistence.Models;
public class PageList<T> : List<T>
{

    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public int PageSize { get; set; }

    public PageList(List<T> items, int currentPage, int pageSize, int totalCount)
    {
        AddRange(items);
        CurrentPage = currentPage;
        PageSize = pageSize;
        TotalCount = totalCount;
        TotalPages = (int)Math.Ceiling(TotalCount / (double)pageSize);
    }

    public async static Task<PageList<T>> CreateAsync(IQueryable<T> query, int currentPage, int pageSize)
    {
        int count = await query.CountAsync();
        List<T> items = await query.Skip((currentPage - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();

        return new PageList<T>(items, currentPage, pageSize, count);

    }

}
