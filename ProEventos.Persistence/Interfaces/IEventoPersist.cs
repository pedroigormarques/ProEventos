using ProEventos.Domain;

namespace ProEventos.Persistence.Interfaces;
public interface IEventoPersist
{
    Task<Evento[]> GetAllEventosAsync(int userId, bool includePalestrantes);
    Task<Evento[]> GetAllEventosByTemaAsync(int userId, string tema, bool includePalestrantes);
    Task<Evento?> GetEventoByIdAsync(int userId, int eventoId, bool includePalestrantes);
}
