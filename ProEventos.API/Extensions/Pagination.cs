using System.Text.Json;
using ProEventos.API.Models;

namespace ProEventos.API.Extensions;

public static class Pagination
{
    public static void AddPagination(this HttpResponse response,
                                     int currentPage,
                                     int pageSize,
                                     int totalCount,
                                     int totalPages)
    {
        PaginationHeader paginationHeader = new PaginationHeader(currentPage, pageSize, totalCount, totalPages);

        var serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader, serializeOptions));
        response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
    }
}
