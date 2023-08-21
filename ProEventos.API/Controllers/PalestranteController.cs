using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Extensions;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;
using ProEventos.Persistence.Models;

namespace ProEventos.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PalestranteController : ControllerBase
{
    private readonly IPalestranteService _palestranteService;
    public PalestranteController(IPalestranteService palestranteService)
    {
        _palestranteService = palestranteService;
    }

    [HttpGet("all", Name = "GetPalestrantes")]
    public async Task<ActionResult<PageList<PalestranteDto>>> Get([FromQuery] PageParams pageParams)
    {
        try
        {
            PageList<PalestranteDto> pageList = await _palestranteService.GetAllPalestrantesAsync(pageParams, true);
            Response.AddPagination(pageList.CurrentPage, pageList.PageSize, pageList.TotalCount, pageList.TotalPages);
            return base.Ok(pageList);
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar palestrantes. Erro: {e.Message}");
        }
    }

    [HttpGet(Name = "GetPalestranteId")]
    public async Task<ActionResult<PalestranteDto>> GetById()
    {
        try
        {
            int userId = User.GetUserId();
            return Ok(await _palestranteService.GetPalestranteByUserIdAsync(userId, true));
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar palestrante. Erro: {e.Message}");
        }
    }

    [HttpPost(Name = "PostPalestrante")]
    public async Task<ActionResult<PalestranteDto>> Post(PalestranteCreateDto model)
    {
        try
        {
            int userId = User.GetUserId();
            return Ok(await _palestranteService.AddPalestrante(userId, model));
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar palestrante. Erro: {e.Message}");
        }

    }

    [HttpPut(Name = "PutPalestrante")]
    public async Task<ActionResult<PalestranteDto>> Put(PalestranteUpdateDto model)
    {
        try
        {
            int userId = User.GetUserId();
            return Ok(await _palestranteService.UpdatePalestrante(userId, model));
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar palestrante. Erro: {e.Message}");
        }
    }
}
