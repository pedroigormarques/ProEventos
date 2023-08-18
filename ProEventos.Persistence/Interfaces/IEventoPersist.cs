using ProEventos.Domain;
using ProEventos.Persistence.Models;

namespace ProEventos.Persistence.Interfaces;
public interface IEventoPersist
{
    Task<PageList<Evento>> GetAllEventosAsync(int userId, PageParams pageParams, bool includePalestrantes);
    Task<Evento?> GetEventoByIdAsync(int userId, int eventoId, bool includePalestrantes);
}
