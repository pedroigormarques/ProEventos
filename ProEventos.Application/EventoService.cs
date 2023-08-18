using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;
using ProEventos.Persistence.Models;

namespace ProEventos.Application
;
public class EventoService : IEventoService
{
    private readonly IGeralPersist _geralPersist;
    private readonly IEventoPersist _eventoPersist;
    private readonly IMapper _mapper;

    public EventoService(IGeralPersist geralPersist, IEventoPersist eventoPersist, IMapper mapper)
    {
        _geralPersist = geralPersist;
        _eventoPersist = eventoPersist;
        _mapper = mapper;
    }

    public async Task<EventoDto> AddEvento(int userId, EventoDto model)
    {
        try
        {
            Evento evento = _mapper.Map<Evento>(model);
            evento.UserId = userId;
            _geralPersist.Add<Evento>(evento);
            await _geralPersist.SaveChangesAsync();

            Evento? resultado = await _eventoPersist.GetEventoByIdAsync(userId, evento.Id, false);
            if (resultado == null) throw new Exception("Erro ao recuperar dados após criação");
            return _mapper.Map<EventoDto>(resultado);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<EventoDto> UpdateEvento(int userId, int eventoId, EventoDto model)
    {
        try
        {
            Evento? evento = await _eventoPersist.GetEventoByIdAsync(userId, eventoId, false);
            if (evento == null) throw NotFoundError();

            _mapper.Map(model, evento);
            _geralPersist.Update<Evento>(evento);
            await _geralPersist.SaveChangesAsync();

            Evento? resultado = await _eventoPersist.GetEventoByIdAsync(userId, eventoId, false);
            if (resultado == null) throw new Exception("Erro ao recuperar dados após atualização");
            return _mapper.Map<EventoDto>(resultado);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<bool> DeleteEvento(int userId, int eventoId)
    {
        try
        {
            Evento? eventoBanco = await _eventoPersist.GetEventoByIdAsync(userId, eventoId, false);
            if (eventoBanco == null) throw NotFoundError();

            _geralPersist.Delete<Evento>(eventoBanco);

            return await _geralPersist.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<PageList<EventoDto>> GetAllEventosAsync(int userId, PageParams pageParams, bool includePalestrantes = false)
    {
        try
        {
            PageList<Evento> pageEventos = await _eventoPersist.GetAllEventosAsync(userId, pageParams, includePalestrantes);

            return new PageList<EventoDto>(_mapper.Map<List<EventoDto>>(pageEventos),
                                            pageEventos.CurrentPage,
                                            pageEventos.PageSize,
                                            pageEventos.TotalCount);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }


    public async Task<EventoDto> GetEventoByIdAsync(int userId, int eventoId, bool includePalestrantes = false)
    {
        try
        {
            Evento? eventoBanco = await _eventoPersist.GetEventoByIdAsync(userId, eventoId, includePalestrantes);
            if (eventoBanco == null) throw NotFoundError();
            return _mapper.Map<EventoDto>(eventoBanco);
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
