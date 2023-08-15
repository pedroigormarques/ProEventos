using ProEventos.Domain;
using Microsoft.EntityFrameworkCore;
using ProEventos.Persistence.Interfaces;
using ProEventos.Persistence.Context;

namespace ProEventos.Persistence;
public class PalestrantePersist : IPalestrantePersist
{
    private readonly ProEventosContext _context;

    public PalestrantePersist(ProEventosContext context)
    {
        _context = context;
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
    {
        IQueryable<Palestrante> query = _context.Palestrantes.Include(p => p.RedesSociais)
                                                             .OrderBy(p => p.Id);

        if (includeEventos)
        {
            query = query.Include(p => p.PalestrantesEventos).ThenInclude(p => p.Evento);
        }

        return await query.ToArrayAsync();
    }

    public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos = false)
    {
        IQueryable<Palestrante> query = _context.Palestrantes.Where(p => p.Nome.ToLower().Contains(nome.ToLower()))
                                                             .Include(p => p.RedesSociais)
                                                             .OrderBy(p => p.Nome.ToLower());

        if (includeEventos)
        {
            query = query.Include(p => p.PalestrantesEventos).ThenInclude(p => p.Evento);
        }

        return await query.ToArrayAsync();
    }

    public async Task<Palestrante?> GetPalestranteByIdAsync(int eventoId, bool includeEventos = false)
    {
        IQueryable<Palestrante> query = _context.Palestrantes.Where(p => p.Id == eventoId)
                                                             .Include(p => p.RedesSociais)
                                                             .OrderBy(p => p.Id);

        if (includeEventos)
        {
            query = query.Include(p => p.PalestrantesEventos).ThenInclude(p => p.Evento);
        }

        return await query.FirstOrDefaultAsync();
    }

}
