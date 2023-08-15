
using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Application;
public class LoteService : ILoteService
{
    private readonly IGeralPersist _geralPersist;
    private readonly ILotePersist _lotePersist;
    private readonly IMapper _mapper;

    public LoteService(IGeralPersist geralPersist, ILotePersist lotePersist, IMapper mapper)
    {
        _geralPersist = geralPersist;
        _lotePersist = lotePersist;
        _mapper = mapper;
    }

    public async Task<LoteDto[]> SaveLotes(int eventoId, LoteDto[] models)
    {
        try
        {
            Lote[] lotes = await _lotePersist.GetAllLotesbyEventoId(eventoId);

            foreach (LoteDto model in models)
            {
                model.EventoId = eventoId;
                if (model.Id == 0)
                {
                    _geralPersist.Add<Lote>(_mapper.Map<Lote>(model));
                }
                else
                {
                    Lote aux = lotes.FirstOrDefault(l => l.Id == model.Id) ?? throw new Exception("Lote a ser atualizado não encontrado");
                    _geralPersist.Update<Lote>(_mapper.Map(model, aux));
                }
            }
            await _geralPersist.SaveChangesAsync();

            Lote[] lotesRetorno = await _lotePersist.GetAllLotesbyEventoId(eventoId);
            return _mapper.Map<LoteDto[]>(lotesRetorno);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<bool> DeleteLote(int eventoId, int loteId)
    {
        try
        {
            Lote? lote = await _lotePersist.GetLotebyIdsAsync(eventoId, loteId);
            if (lote == null) throw new Exception("Lote não encontrado");

            _geralPersist.Delete<Lote>(lote);
            return await _geralPersist.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<LoteDto[]> GetAllLotesByEventoIdAsync(int eventoId)
    {
        try
        {
            Lote[] lotes = await _lotePersist.GetAllLotesbyEventoId(eventoId);
            return _mapper.Map<LoteDto[]>(lotes);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<LoteDto> GetLoteByIdsAsync(int eventoId, int loteId)
    {
        try
        {
            Lote? lote = await _lotePersist.GetLotebyIdsAsync(eventoId, loteId);
            if (lote == null) throw new Exception("Lote não encontrado");

            return _mapper.Map<LoteDto>(lote);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

}
