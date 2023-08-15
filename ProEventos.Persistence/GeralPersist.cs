using Microsoft.EntityFrameworkCore;
using ProEventos.Persistence.Context;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Persistence;
public class GeralPersist : IGeralPersist
{
    private readonly ProEventosContext _context;

    public GeralPersist(ProEventosContext context)
    {
        _context = context;
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public void Add<T>(T entity) where T : class
    {
        _context.Add(entity);
    }

    public void Update<T>(T entity) where T : class
    {
        _context.Update(entity);
    }

    public void Delete<T>(T entity) where T : class
    {
        _context.Remove(entity);
    }

    public async Task<bool> SaveChangesAsync()
    {
        if (await _context.SaveChangesAsync() == 0)
        {
            throw new Exception("Ocorreu algum erro ao salvar");
        }
        return true;
    }
}
