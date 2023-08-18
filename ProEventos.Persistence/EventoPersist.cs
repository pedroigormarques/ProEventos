using ProEventos.Domain;
using Microsoft.EntityFrameworkCore;
using ProEventos.Persistence.Interfaces;
using ProEventos.Persistence.Context;
using ProEventos.Persistence.Models;

namespace ProEventos.Persistence;
public class EventoPersist : IEventoPersist
{
    private readonly ProEventosContext _context;

    public EventoPersist(ProEventosContext context)
    {
        _context = context;
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public async Task<PageList<Evento>> GetAllEventosAsync(int userId, PageParams pageParams,
                                                           bool includePalestrantes = false)
    {
        IQueryable<Evento> query = _context.Eventos.Include(e => e.Lotes)
                                                   .Include(e => e.RedesSociais)
                                                   .Where(e => e.UserId == userId
                                                               && e.Tema.ToLower().Contains(pageParams.Term.ToLower()))
                                                   .OrderBy(e => e.Id);

        if (includePalestrantes)
        {
            query = query.Include(e => e.PalestrantesEventos).ThenInclude(e => e.Palestrante);
        }

        return await PageList<Evento>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
    }

    public async Task<Evento?> GetEventoByIdAsync(int userId, int eventoId, bool includePalestrantes = false)
    {
        IQueryable<Evento> query = _context.Eventos.Where(e => e.UserId == userId && e.Id == eventoId)
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
