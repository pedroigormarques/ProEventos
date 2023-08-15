using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Context;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Persistence;
public class LotePersist : ILotePersist
{
    private readonly ProEventosContext _context;
    public LotePersist(ProEventosContext context)
    {
        _context = context;
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }


    public async Task<Lote[]> GetAllLotesbyEventoId(int eventoId)
    {
        return await _context.Lotes.Where(l => l.EventoId == eventoId)
                                   .OrderBy(e => e.Id)
                                   .ToArrayAsync();
    }
    public async Task<Lote?> GetLotebyIdsAsync(int eventoId, int loteId)
    {
        return await _context.Lotes.Where(l => l.EventoId == eventoId && l.Id == loteId).FirstOrDefaultAsync();
    }
}