using System.Linq.Expressions;

namespace InvenTrac.Repository.IRepository;

public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    Task<T> GetOneAsync(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> GetAllAsync();
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    IEnumerable<T> GetSome(Expression<Func<T, bool>> predicate);
    Task<bool> AddAsync(T entity);
    bool Remove(T entity);
    bool RemoveRange(IEnumerable<T> entity);
    bool Update(T entity);
}

