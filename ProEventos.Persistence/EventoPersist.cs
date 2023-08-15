using ProEventos.Domain;
using Microsoft.EntityFrameworkCore;
using ProEventos.Persistence.Interfaces;
using ProEventos.Persistence.Context;

namespace ProEventos.Persistence;
public class EventoPersist : IEventoPersist
{
    private readonly ProEventosContext _context;

    public EventoPersist(ProEventosContext context)
    {
        _context = context;
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
    {
        IQueryable<Evento> query = _context.Eventos.Include(e => e.Lotes)
                                                   .Include(e => e.RedesSociais)
                                                   .OrderBy(e => e.Id);

        if (includePalestrantes)
        {
            query = query.Include(e => e.PalestrantesEventos).ThenInclude(e => e.Palestrante);
        }

        return await query.ToArrayAsync();
    }

    public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
    {
        IQueryable<Evento> query = _context.Eventos.Where(e => e.Tema.ToLower().Contains(tema.ToLower()))
                                                   .Include(e => e.Lotes)
                                                   .Include(e => e.RedesSociais)
                                                   .OrderBy(e => e.Tema.ToLower());

        if (includePalestrantes)
        {
            query = query.Include(e => e.PalestrantesEventos).ThenInclude(e => e.Palestrante);
        }

        return await query.ToArrayAsync();
    }

    public async Task<Evento?> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
    {
        IQueryable<Evento> query = _context.Eventos.Where(e => e.Id == eventoId)
                                                   .Include(e => e.Lotes)
                                                   .Include(e => e.RedesSociais)
                                                   .OrderBy(e => e.Id);

        if (includePalestrantes)
        {
            query = query.Include(e => e.PalestrantesEventos).ThenInclude(e => e.Palestrante);
        }

        return await query.FirstOrDefaultAsync();
    }
}
