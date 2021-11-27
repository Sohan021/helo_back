using ofarz_rest_api.Domain.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IRepositories.IUserRepositories
{
    public interface IUnionOrWardRepository
    {
        Task<IEnumerable<UnionOrWard>> ListAsync();
        Task<UnionOrWard> FindByIdAsync(int id);
        Task AddAsync(UnionOrWard unionOrWard);
        void Update(UnionOrWard unionOrWard);
        void Remove(UnionOrWard unionOrWard);
    }
}
