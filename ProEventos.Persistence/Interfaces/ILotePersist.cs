using ProEventos.Domain;

namespace ProEventos.Persistence.Interfaces;
public interface ILotePersist
{
    public Task<Lote[]> GetAllLotesbyEventoId(int eventoId);
    public Task<Lote?> GetLotebyIdsAsync(int eventoId, int loteId);
}