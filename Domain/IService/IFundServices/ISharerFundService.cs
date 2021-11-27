using ofarz_rest_api.Domain.IService.Communication.FundCommunication;
using ofarz_rest_api.Domain.Models.Fund;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IService.IFundServices
{
    public interface ISharerFundService
    {
        Task<IEnumerable<SharerFund>> ListAsync();
        Task<SharerFund> FindByIdAsync(int id);
        Task<SaveSharerFundResponse> SaveAsync(SharerFund sharerFund);
        Task<SaveSharerFundResponse> UpdateAsync(int id, SharerFund sharerFund);
        Task<SaveSharerFundResponse> DeleteAsync(int id);
    }
}
