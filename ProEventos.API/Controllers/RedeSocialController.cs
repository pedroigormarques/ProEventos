using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Extensions;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;

namespace ProEventos.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class RedeSocialController : ControllerBase
{
    private readonly IRedeSocialService _redeSocialService;

    public RedeSocialController(IRedeSocialService redeSocialService)
    {
        _redeSocialService = redeSocialService;
    }


    [HttpGet("evento/{eventoId}", Name = "GetRedeSocialByEventoId")]
    public async Task<ActionResult<RedeSocialDto[]>> GetRedeSocialByEventoId(int eventoId)
    {
        try
        {
            return Ok(await _redeSocialService.GetAllByEventoIdAsync(User.GetUserId(), eventoId));
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar redes sociais. Erro: {e.Message}");
        }
    }

    [HttpGet("palestrante", Name = "GetRedeSocialByPalestranteId")]
    public async Task<ActionResult<RedeSocialDto[]>> GetRedeSocialByPalestranteId()
    {
        try
        {
            return Ok(await _redeSocialService.GetAllByPalestranteUserIdAsync(User.GetUserId()));
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar redes sociais. Erro: {e.Message}");
        }
    }

    [HttpPut("evento/{eventoId}", Name = "SaveByEvento")]
    public async Task<ActionResult<RedeSocialDto[]>> SaveByEvento(int eventoId, RedeSocialDto[] models)
    {
        try
        {
            return Ok(await _redeSocialService.SaveByEvento(User.GetUserId(), eventoId, models));
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao salvar redes sociais. Erro: {e.Message}");
        }
    }

    [HttpPut("palestrante", Name = "SaveByPalestrante")]
    public async Task<ActionResult<RedeSocialDto[]>> SaveByPalestrante(RedeSocialDto[] models)
    {
        try
        {
            return Ok(await _redeSocialService.SaveByPalestranteUser(User.GetUserId(), models));
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao salvar redes sociais. Erro: {e.Message}");
        }
    }

    [HttpDelete("evento/{eventoId}/{redeSocialId}", Name = "DeleteByEvento")]
    public async Task<ActionResult<bool>> DeleteByEvento(int eventoId, int redeSocialId)
    {
        try
        {
            return Ok(await _redeSocialService.DeleteByEvento(User.GetUserId(), eventoId, redeSocialId) ? "Removido" : "Erro");
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao remover a rede social. Erro: {e.Message}");
        }
    }

    [HttpDelete("palestrante/{redeSocialId}", Name = "DeleteByPalestrante")]
    public async Task<ActionResult<bool>> DeleteByPalestrante(int redeSocialId)
    {
        try
        {
            return Ok(await _redeSocialService.DeleteByPalestranteUser(User.GetUserId(), redeSocialId) ? "Removido" : "Erro");
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao remover a rede social. Erro: {e.Message}");
        }
    }
}
