using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;

namespace ProEventos.API.Controllers;

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
    public async Task<ActionResult<Evento[]>> Get()
    {
        try
        {
            return Ok(await _eventoService.GetAllEventosAsync());
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar eventos. Erro: {e.Message}");
        }
    }

    [HttpGet("tema/{tema}", Name = "GetEventosTema")]
    public async Task<ActionResult<Evento[]>> GetByTema(string tema)
    {
        try
        {
            return Ok(await _eventoService.GetAllEventosByTemaAsync(tema));
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar eventos. Erro: {e.Message}");
        }
    }

    [HttpGet("{id}", Name = "GetEventoid")]
    public async Task<ActionResult<Evento>> GetById(int id)
    {
        try
        {
            return Ok(await _eventoService.GetEventoByIdAsync(id));
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar evento. Erro: {e.Message}");
        }
    }

    [HttpPost(Name = "PostEvento")]
    public async Task<ActionResult<Evento>> Post(Evento model)
    {
        try
        {
            return Ok(await _eventoService.AddEvento(model));
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar evento. Erro: {e.Message}");
        }

    }

    [HttpPut("{id}", Name = "PutEvento")]
    public async Task<ActionResult<Evento>> Put(int id, Evento model)
    {
        try
        {
            return Ok(await _eventoService.UpdateEvento(id, model));
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
            return Ok(await _eventoService.DeleteEvento(id));
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao remover evento. Erro: {e.Message}");
        }
    }
}
