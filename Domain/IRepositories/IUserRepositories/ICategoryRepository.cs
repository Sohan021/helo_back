using ofarz_rest_api.Domain.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IRepositories.IUserRepositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> ListAsync();
        Task<Category> FindByIdAsync(int id);
        Category GetCategory(int id);
        Task AddAsync(Category category);
        void Update(Category category);
        void Remove(Category category);
    }
}
