using ofarz_rest_api.Domain.IService.Communication.AccountCommunication;
using ofarz_rest_api.Domain.Models.Account;
using ofarz_rest_api.Resources.AuthResources.RoleResources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IService.IAccountServices
{
    public interface IRoleService
    {
        Task<IEnumerable<ApplicationRole>> ListAsync();
        Task<ApplicationRole> FindByIdAsync(string id);
        Task<SaveRoleResponse> SaveAsync(ApplicationRole applicationRole);
        Task<SaveRoleResponse> UpdateAsync(string id, RoleResource roleResource);
        Task<SaveRoleResponse> DeleteAsync(string id);
    }
}
