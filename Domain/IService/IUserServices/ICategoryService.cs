using ofarz_rest_api.Domain.IService.Communication.UserCommunication;
using ofarz_rest_api.Domain.Models.User;
using ofarz_rest_api.Resources.UserResources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IService.IUserServices
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> ListAsync();
        Task<Category> FindByIdAsync(int id);
        Category GetCategory(int id);
        Task<SaveCategoryResponse> SaveAsync(Category category);
        Task<SaveCategoryResponse> UpdateAsync(int id, CategoryResource category);
        Task<SaveCategoryResponse> DeleteAsync(int id);
    }
}
