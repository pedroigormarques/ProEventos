using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Context;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Persistence;
public class RedeSocialPersist : GeralPersist, IRedeSocialPersist
{
    private readonly ProEventosContext _context;

    public RedeSocialPersist(ProEventosContext context) : base(context)
    {
        _context = context;
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public async Task<RedeSocial[]> GetAllByEventoIdAsync(int enventoId)
    {
        return await _context.RedesSociais.Where(rs => rs.EventoId == enventoId)
                                          .ToArrayAsync();
    }

    public async Task<RedeSocial[]> GetAllByPalestranteIdAsync(int palestranteId)
    {
        return await _context.RedesSociais.Where(rs => rs.PalestranteId == palestranteId)
                                          .ToArrayAsync();
    }

    public async Task<RedeSocial?> GetRedeSocialEventoByIdsAsync(int enventoId, int id)
    {
        return await _context.RedesSociais.Where(rs => rs.EventoId == enventoId && rs.Id == id)
                                          .FirstOrDefaultAsync();
    }

    public async Task<RedeSocial?> GetRedeSocialPalestranteByIdsAsync(int palestranteId, int id)
    {
        return await _context.RedesSociais.Where(rs => rs.PalestranteId == palestranteId && rs.Id == id)
                                          .FirstOrDefaultAsync();
    }
}
