using ofarz_rest_api.Domain.IService.Communication.FundCommunication;
using ofarz_rest_api.Domain.Models.Fund;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IService.IFundServices
{
    public interface ICustomerFundService
    {
        Task<IEnumerable<ShoperFund>> ListAsync();
        Task<ShoperFund> FindByIdAsync(int id);
        Task<SaveCustomerFundResponse> SaveAsync(ShoperFund customerFund);
        Task<SaveCustomerFundResponse> UpdateAsync(int id, ShoperFund customerFund);
        Task<SaveCustomerFundResponse> DeleteAsync(int id);
    }
}
