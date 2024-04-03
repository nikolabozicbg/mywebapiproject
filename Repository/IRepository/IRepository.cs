using System.Linq.Expressions;

namespace MyWebApiProject.Repository.IRepository;

public interface IRepository<T> where T : class
{
    Task CreateAsync(T entity);
    Task Remove(T entity);
    Task SaveAsync();
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
    Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool track = false);
}