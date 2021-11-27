using ofarz_rest_api.Domain.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IRepositories.IUserRepositories
{
    public interface IDistrictRepository
    {
        Task<IEnumerable<District>> ListAsync();
        Task<District> FindByIdAsync(int id);
        District GetDistrict(int id);
        Task AddAsync(District district);
        void Update(District district);
        void Remove(District district);
    }
}
