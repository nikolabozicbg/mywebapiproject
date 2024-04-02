using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyWebApiProject.Data;
using MyWebApiProject.Repository.IRepository;

namespace MyWebApiProject.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly AppliationDbContext _db;
    internal DbSet<T> dbSet;


    public Repository(AppliationDbContext db)
    {
        _db = db;
        dbSet = _db.Set<T>();
    }


    public Task Remove(T entity)
    {
        throw new NotImplementedException();
    }

    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }

    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
    {
        IQueryable<T> queryable = dbSet;
        if (filter != null) queryable = queryable.Where(filter);
        return await queryable.ToListAsync();
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool track = false)
    {
        IQueryable<T> queryable = dbSet;
        if (!track) queryable = queryable.AsNoTracking();

        if (filter != null) queryable = queryable.Where(filter);
        return await queryable.FirstOrDefaultAsync();
    }

    public async Task CreateAsync(T entity)
    {
        await dbSet.AddAsync(entity);
        await SaveAsync();
    }


    public async Task UpdateAsync(T entity)
    {
        dbSet.Update(entity);
        await SaveAsync();
    }
}