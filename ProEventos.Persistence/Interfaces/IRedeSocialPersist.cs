using ProEventos.Domain;

namespace ProEventos.Persistence.Interfaces
{
    public interface IRedeSocialPersist : IGeralPersist
    {
        Task<RedeSocial?> GetRedeSocialEventoByIdsAsync(int enventoId, int id);
        Task<RedeSocial?> GetRedeSocialPalestranteByIdsAsync(int palestranteId, int id);
        Task<RedeSocial[]> GetAllByEventoIdAsync(int enventoId);
        Task<RedeSocial[]> GetAllByPalestranteIdAsync(int palestranteId);
    }
}