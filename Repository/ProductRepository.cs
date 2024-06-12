using Microsoft.EntityFrameworkCore;
using WexAssessmentApi.Interfaces;


namespace WexAssessmentApi.Repository
{
    public class ProductRepository : Repository<Product?>, IProductRepository
    {
        public ProductRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product?>> GetProductsByCategoryAsync(string category)
        {
            var products = await GetAllAsync();

            return products.Where(p => p.Category == category).AsEnumerable<Product?>();
        }

        //public override async Task<Product?> GetAsync(int? id)
        //{
        //    var products = await base.GetAllAsync();
        //    return _context.Products.Local.FirstOrDefault(p => p.Id == 3);
        //}

        //public override async Task UpdateAsync(int? id, Product? entity)
        //{
        //    var product = GetAsync(id);

        //    if (product != null) 
        //    {
        //        await base.DeleteAsync(id);
        //        await base.AddAsync(entity);
        //    }


        //}
    }
}
