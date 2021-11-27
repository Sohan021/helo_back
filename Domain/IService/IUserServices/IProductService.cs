using ofarz_rest_api.Domain.IService.Communication.UserCommunication;
using ofarz_rest_api.Domain.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IService.IUserServices
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> ListAsync();
        Task<Product> FindByIdAsync(int id);
        Task<SaveProductResponse> SaveAsync(Product product);
        Task<SaveProductResponse> UpdateAsync(int id, Product product);
        Task<SaveProductResponse> DeleteAsync(int id);

    }
}
