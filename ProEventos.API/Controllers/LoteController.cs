using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;

namespace ProEventos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoteController : ControllerBase
{
    private readonly ILoteService _loteService;
    public LoteController(ILoteService loteService)
    {
        _loteService = loteService;
    }


    [HttpGet("{eventoId}", Name = "GetLoteByEventoId")]
    public async Task<ActionResult<LoteDto[]>> GetLoteByEventoId(int eventoId)
    {
        try
        {
            return Ok(await _loteService.GetAllLotesByEventoIdAsync(eventoId));
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar lotes. Erro: {e.Message}");
        }
    }

    [HttpPut("{eventoId}", Name = "SaveLotes")]
    public async Task<ActionResult<LoteDto[]>> SaveLotes(int eventoId, LoteDto[] models)
    {
        try
        {
            return Ok(await _loteService.SaveLotes(eventoId, models));
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao salvar lotes. Erro: {e.Message}");
        }
    }

    [HttpDelete("{eventoId}/{loteId}", Name = "DeleteLote")]
    public async Task<ActionResult<bool>> Delete(int eventoId, int loteId)
    {
        try
        {
            return Ok(await _loteService.DeleteLote(eventoId, loteId) ? "Removido" : "Erro");
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao remover lote. Erro: {e.Message}");
        }
    }
}
