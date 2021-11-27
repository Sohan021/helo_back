using ofarz_rest_api.Domain.IService.Communication.UserCommunication;
using ofarz_rest_api.Domain.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IService.IUserServices
{
    public interface IProductTypeService
    {
        Task<IEnumerable<ProductType>> ListAsync();
        Task<ProductType> FindByIdAsync(int id);
        Task<SaveProductTypeResponse> SaveAsync(ProductType productType);
        Task<SaveProductTypeResponse> UpdateAsync(int id, ProductType productType);
        Task<SaveProductTypeResponse> DeleteAsync(int id);
    }
}
