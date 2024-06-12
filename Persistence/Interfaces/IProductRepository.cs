using Domain.Primitives;
using Domain.Products;
using Persistence.Repositories;

namespace Persistence.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category);
    }
}
