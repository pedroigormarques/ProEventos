using ProEventos.Domain;
using Microsoft.EntityFrameworkCore;
using ProEventos.Persistence.Interfaces;
using ProEventos.Persistence.Context;
using ProEventos.Persistence.Models;

namespace ProEventos.Persistence;
public class PalestrantePersist : GeralPersist, IPalestrantePersist
{
    private readonly ProEventosContext _context;

    public PalestrantePersist(ProEventosContext context) : base(context)
    {
        _context = context;
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public async Task<PageList<Palestrante>> GetAllPalestrantesAsync(PageParams pageParams, bool includeEventos = false)
    {
        IQueryable<Palestrante> query = _context.Palestrantes.Where(
            p => p.User.Funcao == Domain.Enum.Funcao.Palestrante &&
            (p.User.PrimeiroNome.ToLower().Contains(pageParams.Term.ToLower()) ||
            p.User.UltimoNome.ToLower().Contains(pageParams.Term.ToLower()) ||
            p.MiniCurriculo.ToLower().Contains(pageParams.Term.ToLower()))
            )
            .Include(p => p.RedesSociais)
            .Include(p => p.User)
            .OrderBy(p => p.User.PrimeiroNome.ToLower());

        if (includeEventos)
        {
            query = query.Include(p => p.PalestrantesEventos).ThenInclude(p => p.Evento);
        }

        return await PageList<Palestrante>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
    }

    public async Task<Palestrante?> GetPalestranteByUserIdAsync(int userId, bool includeEventos = false)
    {
        IQueryable<Palestrante> query = _context.Palestrantes.Where(p => p.UserId == userId)
                                                             .Include(p => p.RedesSociais)
                                                             .Include(p => p.User)
                                                             .OrderBy(p => p.Id);

        if (includeEventos)
        {
            query = query.Include(p => p.PalestrantesEventos).ThenInclude(p => p.Evento);
        }

        return await query.FirstOrDefaultAsync();
    }

}
