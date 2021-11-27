using Microsoft.EntityFrameworkCore;
using ofarz_rest_api.Domain.IRepositories.IUserRepositories;
using ofarz_rest_api.Domain.Models.User;
using ofarz_rest_api.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ofarz_rest_api.Persistence.Repositories.UserRepositories
{
    public class ProductTypeRepository : BaseRepository, IProductTypeRepository
    {
        public ProductTypeRepository(AppDbContext context) : base(context)
        {

        }

        public async Task AddAsync(ProductType productType)
        {
            await _context.ProductTypes.AddAsync(productType);
        }

        public async Task<ProductType> FindByIdAsync(int id)
        {
            return await _context.ProductTypes.FindAsync(id);
        }

        public ProductType GetProductType(int id)
        {
            return _context.ProductTypes.Where(pt => pt.Id == id).FirstOrDefault();
        }

        public async Task<IEnumerable<ProductType>> ListAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }

        public void Remove(ProductType productType)
        {
            _context.ProductTypes.Remove(productType);
        }

        public void Update(ProductType productType)
        {
            _context.ProductTypes.Update(productType);
        }
    }
}
