using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;
using ProEventos.Persistence.Models;

namespace ProEventos.Application;
public class PalestranteService : IPalestranteService
{
    private readonly IPalestrantePersist _palestrantePersist;
    private readonly IMapper _mapper;

    public PalestranteService(IPalestrantePersist palestrantePersist, IMapper mapper)
    {
        _mapper = mapper;
        _palestrantePersist = palestrantePersist;
    }

    public async Task<PalestranteDto> AddPalestrante(int userId, PalestranteCreateDto model)
    {
        try
        {
            Palestrante palestrante = _mapper.Map<Palestrante>(model);
            palestrante.UserId = userId;

            _palestrantePersist.Add<Palestrante>(palestrante);
            await _palestrantePersist.SaveChangesAsync();

            Palestrante? palestranteRetorno = await _palestrantePersist.GetPalestranteByUserIdAsync(userId, false);
            if (palestranteRetorno == null) throw new Exception("Erro ao recuperar palestrante após criação");

            return _mapper.Map<PalestranteDto>(palestranteRetorno);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<PalestranteDto> UpdatePalestrante(int userId, PalestranteUpdateDto model)
    {
        try
        {
            Palestrante? palestrante = await _palestrantePersist.GetPalestranteByUserIdAsync(userId, false);
            if (palestrante == null) throw new Exception("Palestrante não encontrado");

            model.Id = palestrante.Id;
            model.UserId = userId;
            _mapper.Map(model, palestrante);

            _palestrantePersist.Update<Palestrante>(palestrante);
            await _palestrantePersist.SaveChangesAsync();

            Palestrante? palestranteRetorno = await _palestrantePersist.GetPalestranteByUserIdAsync(userId, false);
            if (palestranteRetorno == null) throw new Exception("Erro ao recuperar palestrante após alteração");

            return _mapper.Map<PalestranteDto>(palestranteRetorno);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<PageList<PalestranteDto>> GetAllPalestrantesAsync(PageParams pageParams, bool includeEventos = false)
    {
        try
        {
            PageList<Palestrante> palestrantes = await _palestrantePersist.GetAllPalestrantesAsync(pageParams, includeEventos);

            return new PageList<PalestranteDto>(
                _mapper.Map<List<PalestranteDto>>(palestrantes),
                palestrantes.CurrentPage,
                palestrantes.PageSize,
                palestrantes.TotalCount);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<PalestranteDto> GetPalestranteByUserIdAsync(int userId, bool includeEventos = false)
    {
        try
        {
            Palestrante? palestrante = await _palestrantePersist.GetPalestranteByUserIdAsync(userId, includeEventos);
            if (palestrante == null) throw new Exception("Palestrante não encontrado");

            return _mapper.Map<PalestranteDto>(palestrante);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}
