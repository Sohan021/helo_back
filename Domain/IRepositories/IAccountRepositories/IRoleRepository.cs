using ofarz_rest_api.Domain.Models.Account;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IRepositories.IAccountRepositories
{
    public interface IRoleRepository
    {
        Task<IEnumerable<ApplicationRole>> ListOfRoles();
        Task<IEnumerable<ApplicationUser>> ListOfUserInRole(string id);

        Task<ApplicationRole> FindByIdAsync(string id);
        Task AddRole(ApplicationRole applicationRole);
        void UpdateRole(ApplicationRole applicationRole);
        void RemoveRole(ApplicationRole applicationRole);
    }
}
