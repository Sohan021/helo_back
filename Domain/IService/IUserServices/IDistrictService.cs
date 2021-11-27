using ofarz_rest_api.Domain.IService.Communication.UserCommunication;
using ofarz_rest_api.Domain.Models.User;
using ofarz_rest_api.Resources.UserResources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IService.IUserServices
{
    public interface IDistrictService
    {
        Task<IEnumerable<District>> ListAsync();
        Task<District> FindByIdAsync(int id);
        District GetDistrict(int id);
        Task<SaveDistrictResponse> SaveAsync(District district);
        Task<SaveDistrictResponse> UpdateAsync(int id, DistrictResource district);
        Task<SaveDistrictResponse> DeleteAsync(int id);
    }
}
