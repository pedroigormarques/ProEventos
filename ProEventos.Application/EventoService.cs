using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Application
;
public class EventoService : IEventoService
{
    private readonly IGeralPersist _geralPersist;
    private readonly IEventoPersist _eventoPersist;

    public EventoService(IGeralPersist geralPersist, IEventoPersist eventoPersist)
    {
        _geralPersist = geralPersist;
        _eventoPersist = eventoPersist;
    }

    public async Task<Evento> AddEvento(Evento model)
    {
        try
        {
            _geralPersist.Add<Evento>(model);
            await _geralPersist.SaveChangesAsync();

            return await _eventoPersist.GetEventoByIdAsync(model.Id, false)
                   ?? throw new Exception("Erro ao recuperar dados após criação");
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<Evento> UpdateEvento(int eventoId, Evento model)
    {
        try
        {
            Evento? eventoBanco = await _eventoPersist.GetEventoByIdAsync(eventoId, false);
            if (eventoBanco == null) throw NotFoundError();

            model.Id = eventoId;

            _geralPersist.Update<Evento>(model);
            await _geralPersist.SaveChangesAsync();

            return await _eventoPersist.GetEventoByIdAsync(eventoId, false)
                   ?? throw new Exception("Erro ao recuperar dados após atualização");
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<bool> DeleteEvento(int eventoId)
    {
        try
        {
            Evento? eventoBanco = await _eventoPersist.GetEventoByIdAsync(eventoId, false);
            if (eventoBanco == null) throw NotFoundError();

            _geralPersist.Delete<Evento>(eventoBanco);

            return await _geralPersist.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
    {
        try
        {
            return await _eventoPersist.GetAllEventosAsync(includePalestrantes);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
    {
        try
        {
            return await _eventoPersist.GetAllEventosByTemaAsync(tema, includePalestrantes);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
    {
        try
        {
            return await _eventoPersist.GetEventoByIdAsync(eventoId, includePalestrantes) ?? throw NotFoundError();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private Exception NotFoundError()
    {
        return new Exception("Evento não encontrado");
    }

}
