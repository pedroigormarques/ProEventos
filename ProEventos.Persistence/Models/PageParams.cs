
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Persistence.Models;
public class PageParams
{
    private const int MaxPageSize = 50;

    [Range(1, int.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
    public int PageNumber { get; set; } = 1;

    [Range(1, 50)]
    private int pageSize = 10;
    public int PageSize
    {
        get => pageSize;
        set => pageSize = value > MaxPageSize ? MaxPageSize : value;
    }

    public string Term { get; set; } = "";
}
