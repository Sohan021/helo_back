using ofarz_rest_api.Domain.IService.Communication.UserCommunication;
using ofarz_rest_api.Domain.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IService.IUserServices
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> ListAsync();
        Task<Country> FindByIdAsync(int id);
        Country GetCountry(int id);
        Task<SaveCountryResponse> SaveAsync(Country country);
        Task<SaveCountryResponse> UpdateAsync(int id, Country country);
        Task<SaveCountryResponse> DeleteAsync(int id);
    }
}
