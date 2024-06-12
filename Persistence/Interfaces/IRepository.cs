namespace Persistence.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T?>> GetAllAsync(int page, int pageSize);
        Task<T?> GetByIdAsync(int? id);
        Task AddAsync(T? entity);
        Task UpdateAsync(T? entity);
        Task DeleteAsync(int? id);
    }
}
