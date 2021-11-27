using ofarz_rest_api.Domain.Models.Fund;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IRepositories.IFundRepository
{
    public interface ICustomerFundRepository
    {
        Task<IEnumerable<ShoperFund>> ListAsync();
        Task<ShoperFund> FindByIdAsync(int id);
        Task AddAsync(ShoperFund customerFund);
        void Update(ShoperFund customerFund);
        void Remove(ShoperFund customerFund);
    }
}
