using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Data;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventoController : ControllerBase
{
    private readonly DataContext _context;
    public EventoController(DataContext context)
    {
        _context = context;
    }

    [HttpGet(Name = "GetEvento")]
    public IEnumerable<Evento> Get()
    {
        return _context.Eventos;
    }

    [HttpGet("{id}", Name = "GetEventoid")]
    public Evento GetById(int id)
    {
        return _context.Eventos.First(a => a.EventoId == id);
    }

    [HttpPost(Name = "PostEvento")]
    public Evento Post()

    {
        return _context.Eventos.First();
    }

    [HttpPut("{id}", Name = "PutEvento")]
    public Evento Put(int id)
    {
        return _context.Eventos.First(a => a.EventoId == id); ;
    }

    [HttpDelete("{id}", Name = "DeleteEvento")]
    public Evento Delete(int id)
    {
        return _context.Eventos.First(a => a.EventoId == id); ;
    }
}
