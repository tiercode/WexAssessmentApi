using System.Collections.Generic;

namespace WexAssessmentApi.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetAsync(int? id);
        Task AddAsync (T? entity);
        Task UpdateAsync (int? id, T? entity);
        Task DeleteAsync(int? id);
    }
}
