using ofarz_rest_api.Domain.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IRepositories.IUserRepositories
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> ListAsync();
        Task<Country> FindByIdAsync(int id);
        Country GetCountry(int id);
        Task AddAsync(Country country);
        void Update(Country country);
        void Remove(Country country);
    }
}
