using ofarz_rest_api.Domain.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IRepositories.IUserRepositories
{
    public interface IUpozillaRepository
    {
        Task<IEnumerable<Upozila>> ListAsync();
        Task<Upozila> FindByIdAsync(int id);
        Upozila GetUpozilla(int id);
        Task AddAsync(Upozila upozilla);
        void Update(Upozila upozilla);
        void Remove(Upozila upozilla);
    }
}
