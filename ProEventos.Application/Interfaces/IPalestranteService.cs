
using ProEventos.Application.Dtos;
using ProEventos.Persistence.Models;

namespace ProEventos.Application.Interfaces;
public interface IPalestranteService
{
    Task<PalestranteDto> AddPalestrante(int userId, PalestranteCreateDto model);

    Task<PalestranteDto> UpdatePalestrante(int userId, PalestranteUpdateDto model);

    Task<PageList<PalestranteDto>> GetAllPalestrantesAsync(PageParams pageParams, bool includeEventos = false);
    Task<PalestranteDto> GetPalestranteByUserIdAsync(int userId, bool includeEventos = false);
}
