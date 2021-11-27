using ofarz_rest_api.Domain.IService.Communication.UserCommunication;
using ofarz_rest_api.Domain.Models.User;
using ofarz_rest_api.Resources.UserResources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IService.IUserServices
{
    public interface IDivisionService
    {
        Task<IEnumerable<Division>> ListAsync();
        Task<Division> FindByIdAsync(int id);
        Division GetDivision(int id);
        Task<SaveDivisionResponse> SaveAsync(Division division);
        Task<SaveDivisionResponse> UpdateAsync(int id, DivisionResource division);
        Task<SaveDivisionResponse> DeleteAsync(int id);
    }
}
