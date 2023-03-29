using Microsoft.EntityFrameworkCore;

namespace FitnessClub.DAL.FitnessClubDataBase;

public class BaseRepository<T> : IDisposable where T : BaseEntity, new()
{
    private readonly DbSet<T> _table;
    private readonly FitnessClubContext _context;

    protected FitnessClubContext Context => _context;

    public BaseRepository(FitnessClubContext context)
    {
        _context = context;
        _table = context.Set<T>();
    }

    public async Task<T> AddAsync(T entity)
    {
        await _table.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<T>> AddAsync(IEnumerable<T> entities)
    {
        await _table.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
        return entities;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _table.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<T>> UpdateAsync(IEnumerable<T> entities)
    {
        _table.UpdateRange(entities);
        await _context.SaveChangesAsync();
        return entities;
    }

    public async Task DeleteAsync(T entity)
    {
        _table.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(IEnumerable<T> entities)
    {
        _table.RemoveRange(entities);
        await _context.SaveChangesAsync();
    }

    public void Dispose() =>
        _context.Dispose();

    public async Task SaveChangesAsync() =>
        await _context.SaveChangesAsync();
}
