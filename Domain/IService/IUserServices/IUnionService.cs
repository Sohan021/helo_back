using ofarz_rest_api.Domain.IService.Communication.UserCommunication;
using ofarz_rest_api.Domain.Models.User;
using ofarz_rest_api.Resources.UserResources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IService.IUserServices
{
    public interface IUnionService
    {
        Task<IEnumerable<UnionOrWard>> ListAsync();
        Task<UnionOrWard> FindByIdAsync(int id);
        Task<SaveUnionOrWardResponse> SaveAsync(UnionOrWard unionOrWard);
        Task<SaveUnionOrWardResponse> UpdateAsync(int id, UnionOrWardResource unionOrWard);
        Task<SaveUnionOrWardResponse> DeleteAsync(int id);
    }
}
