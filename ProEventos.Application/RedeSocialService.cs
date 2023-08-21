
using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Application;
public class RedeSocialService : IRedeSocialService
{
    private readonly IRedeSocialPersist _redeSocialPersist;
    private readonly IMapper _mapper;
    private readonly IEventoPersist _eventoPersist;
    private readonly IPalestrantePersist _palestrantePersist;

    public RedeSocialService(
        IRedeSocialPersist redeSocialPersist,
        IMapper mapper,
        IEventoPersist eventoPersist,
        IPalestrantePersist palestrantePersist)
    {
        _redeSocialPersist = redeSocialPersist;
        _mapper = mapper;
        _eventoPersist = eventoPersist;
        _palestrantePersist = palestrantePersist;
    }

    public async Task<RedeSocialDto[]> SaveByEvento(int userId, int eventoId, RedeSocialDto[] models)
    {
        try
        {
            await VerificarAutoridadeDoEvento(userId, eventoId);

            RedeSocial[] redesSociais = await _redeSocialPersist.GetAllByEventoIdAsync(eventoId);

            foreach (RedeSocialDto model in models)
            {
                model.EventoId = eventoId;
                model.PalestranteId = null;
                if (model.Id == 0)
                {
                    _redeSocialPersist.Add<RedeSocial>(_mapper.Map<RedeSocial>(model));
                }
                else
                {
                    RedeSocial aux = redesSociais.FirstOrDefault(l => l.Id == model.Id) ?? throw new Exception("Rede social a ser atualizada não encontrada");
                    _redeSocialPersist.Update<RedeSocial>(_mapper.Map(model, aux));
                }
            }
            await _redeSocialPersist.SaveChangesAsync();

            RedeSocial[] RedesSociaisRetorno = await _redeSocialPersist.GetAllByEventoIdAsync(eventoId);
            return _mapper.Map<RedeSocialDto[]>(RedesSociaisRetorno);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<bool> DeleteByEvento(int userId, int eventoId, int id)
    {
        try
        {
            await VerificarAutoridadeDoEvento(userId, eventoId);

            RedeSocial? redeSocial = await _redeSocialPersist.GetRedeSocialEventoByIdsAsync(eventoId, id);
            if (redeSocial == null) throw new Exception("Rede social não encontrada");

            _redeSocialPersist.Delete<RedeSocial>(redeSocial);
            return await _redeSocialPersist.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<RedeSocialDto[]> GetAllByEventoIdAsync(int userId, int eventoId)
    {
        try
        {
            await VerificarAutoridadeDoEvento(userId, eventoId);

            RedeSocial[] redesSociais = await _redeSocialPersist.GetAllByEventoIdAsync(eventoId);
            return _mapper.Map<RedeSocialDto[]>(redesSociais);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<RedeSocialDto> GetRedeSocialEventoByIdsAsync(int userId, int eventoId, int id)
    {
        try
        {
            await VerificarAutoridadeDoEvento(userId, eventoId);

            RedeSocial? redeSocial = await _redeSocialPersist.GetRedeSocialEventoByIdsAsync(eventoId, id);
            if (redeSocial == null) throw new Exception("Rede social não encontrada");

            return _mapper.Map<RedeSocialDto>(redeSocial);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<RedeSocialDto[]> SaveByPalestranteUser(int userId, RedeSocialDto[] models)
    {
        try
        {
            Palestrante palestrante = await CarregarPalestrante(userId);

            RedeSocial[] redesSociais = await _redeSocialPersist.GetAllByPalestranteIdAsync(palestrante.Id);

            foreach (RedeSocialDto model in models)
            {
                model.PalestranteId = palestrante.Id;
                model.EventoId = null;
                if (model.Id == 0)
                {
                    _redeSocialPersist.Add<RedeSocial>(_mapper.Map<RedeSocial>(model));
                }
                else
                {
                    RedeSocial aux = redesSociais.FirstOrDefault(l => l.Id == model.Id)
                                     ?? throw new Exception("Rede social a ser atualizada não encontrada");
                    _redeSocialPersist.Update<RedeSocial>(_mapper.Map(model, aux));
                }
            }
            await _redeSocialPersist.SaveChangesAsync();

            RedeSocial[] RedesSociaisRetorno = await _redeSocialPersist.GetAllByPalestranteIdAsync(palestrante.Id);
            return _mapper.Map<RedeSocialDto[]>(RedesSociaisRetorno);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<bool> DeleteByPalestranteUser(int userId, int id)
    {
        try
        {
            Palestrante palestrante = await CarregarPalestrante(userId);

            RedeSocial redeSocial = await _redeSocialPersist.GetRedeSocialPalestranteByIdsAsync(palestrante.Id, id)
                                     ?? throw new Exception("Rede social não encontrada");

            _redeSocialPersist.Delete<RedeSocial>(redeSocial);
            return await _redeSocialPersist.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<RedeSocialDto[]> GetAllByPalestranteUserIdAsync(int userId)
    {
        try
        {
            Palestrante palestrante = await CarregarPalestrante(userId);

            RedeSocial[] redesSociais = await _redeSocialPersist.GetAllByPalestranteIdAsync(palestrante.Id);
            return _mapper.Map<RedeSocialDto[]>(redesSociais);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<RedeSocialDto> GetRedeSocialPalestranteUserByIdsAsync(int userId, int id)
    {
        try
        {
            Palestrante palestrante = await CarregarPalestrante(userId);

            RedeSocial redeSocial = await _redeSocialPersist.GetRedeSocialPalestranteByIdsAsync(palestrante.Id, id)
                                    ?? throw new Exception("Rede social não encontrada");

            return _mapper.Map<RedeSocialDto>(redeSocial);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }


    private async Task VerificarAutoridadeDoEvento(int userId, int eventoId)
    {
        Evento evento = await _eventoPersist.GetEventoByIdAsync(userId, eventoId, false)
                        ?? throw new Exception("Evento não encontrado ou usuário não é o criador");
    }

    private async Task<Palestrante> CarregarPalestrante(int userId)
    {
        return await _palestrantePersist.GetPalestranteByUserIdAsync(userId)
                        ?? throw new Exception("Usuário não é um palestrante");
    }

}
