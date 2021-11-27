using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ofarz_rest_api.Domain.IRepositories.IUserRepositories;
using ofarz_rest_api.Domain.Models.User;
using ofarz_rest_api.Persistence.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ofarz_rest_api.Persistence.Repositories.UserRepositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        private readonly ILogger _logger;

        public ProductRepository(AppDbContext context, ILoggerFactory loggerFactory) : base(context)
        {

            _logger = loggerFactory.CreateLogger("FileRepository");
        }
        public async Task<IEnumerable<Product>> ListAsync()
        {
            return await _context.Products
                        .Include(p => p.Category)
                        .Include(p => p.ProductType)
                        .Include(_ => _.SubCategory)
                        .ToListAsync();
            //return await _context.Products.ToListAsync();
        }
        public async Task<Product> FindByIdAsync(int id)
        {
            return await _context.Products
                        .Include(p => p.Category)
                        .Include(p => p.ProductType)
                        .Include(_ => _.SubCategory)
                        .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);

        }
        public void Update(Product product)
        {
            _context.Products.Update(product);
        }
        public void Remove(Product product)
        {
            _context.Products.Remove(product);
        }

    }

}
