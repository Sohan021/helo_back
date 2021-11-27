using ofarz_rest_api.Domain.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IRepositories.IUserRepositories
{
    public interface IDivisionRepository
    {

        Task<IEnumerable<Division>> ListAsync();
        Task<Division> FindByIdAsync(int id);
        Division GetDivision(int id);
        Task AddAsync(Division division);
        void Update(Division division);
        void Remove(Division division);
    }
}
