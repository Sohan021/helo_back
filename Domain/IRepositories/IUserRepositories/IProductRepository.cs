using ofarz_rest_api.Domain.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IRepositories.IUserRepositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> ListAsync();
        Task<Product> FindByIdAsync(int id);
        Task AddAsync(Product product);
        void Update(Product product);
        void Remove(Product product);
    }
}
