using ProEventos.Application.Dtos;

namespace ProEventos.Application.Interfaces;
public interface IRedeSocialService
{
    Task<RedeSocialDto[]> SaveByEvento(int userId, int eventoId, RedeSocialDto[] models);
    Task<bool> DeleteByEvento(int userId, int eventoId, int id);
    Task<RedeSocialDto> GetRedeSocialEventoByIdsAsync(int userId, int eventoId, int id);
    Task<RedeSocialDto[]> GetAllByEventoIdAsync(int userId, int eventoId);

    Task<RedeSocialDto[]> SaveByPalestranteUser(int userId, RedeSocialDto[] models);
    Task<bool> DeleteByPalestranteUser(int userId, int id);
    Task<RedeSocialDto> GetRedeSocialPalestranteUserByIdsAsync(int userId, int id);
    Task<RedeSocialDto[]> GetAllByPalestranteUserIdAsync(int userId);
}
