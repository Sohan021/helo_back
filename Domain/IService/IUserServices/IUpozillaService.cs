using ofarz_rest_api.Domain.IService.Communication.UserCommunication;
using ofarz_rest_api.Domain.Models.User;
using ofarz_rest_api.Resources.UserResources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IService.IUserServices
{
    public interface IUpozillaService
    {
        Task<IEnumerable<Upozila>> ListAsync();
        Task<Upozila> FindByIdAsync(int id);
        Upozila GetUpozilla(int id);
        Task<SaveUpozillaResponse> SaveAsync(Upozila upozilla);
        Task<SaveUpozillaResponse> UpdateAsync(int id, UpozilaResource upozilla);
        Task<SaveUpozillaResponse> DeleteAsync(int id);
    }
}
