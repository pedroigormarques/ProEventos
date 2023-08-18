using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Extensions;
using ProEventos.API.Models;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;
using ProEventos.Persistence.Models;

namespace ProEventos.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EventoController : ControllerBase
{
    private readonly IEventoService _eventoService;
    public EventoController(IEventoService eventoService)
    {
        _eventoService = eventoService;
    }

    [HttpGet(Name = "GetEventos")]
    public async Task<ActionResult<PageList<EventoDto>>> Get([FromQuery] PageParams pageParams)
    {
        try
        {
            int userId = User.GetUserId();
            PageList<EventoDto> pageList = await _eventoService.GetAllEventosAsync(userId, pageParams);
            Response.AddPagination(pageList.CurrentPage, pageList.PageSize, pageList.TotalCount, pageList.TotalPages);
            return base.Ok(pageList);
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar eventos. Erro: {e.Message}");
        }
    }

    [HttpGet("{id}", Name = "GetEventoid")]
    public async Task<ActionResult<EventoDto>> GetById(int id)
    {
        try
        {
            int userId = User.GetUserId();
            return Ok(await _eventoService.GetEventoByIdAsync(userId, id));
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar evento. Erro: {e.Message}");
        }
    }

    [HttpPost(Name = "PostEvento")]
    public async Task<ActionResult<EventoDto>> Post(EventoDto model)
    {
        try
        {
            int userId = User.GetUserId();
            return Ok(await _eventoService.AddEvento(userId, model));
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar evento. Erro: {e.Message}");
        }

    }

    [HttpPut("{id}", Name = "PutEvento")]
    public async Task<ActionResult<EventoDto>> Put(int id, EventoDto model)
    {
        try
        {
            int userId = User.GetUserId();
            return Ok(await _eventoService.UpdateEvento(userId, id, model));
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar evento. Erro: {e.Message}");
        }
    }

    [HttpDelete("{id}", Name = "DeleteEvento")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        try
        {
            int userId = User.GetUserId();
            return Ok(await _eventoService.DeleteEvento(userId, id));
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao remover evento. Erro: {e.Message}");
        }
    }
}
