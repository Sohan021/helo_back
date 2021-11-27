using ofarz_rest_api.Domain.IService.Communication.FundCommunication;
using ofarz_rest_api.Domain.Models.Fund;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IService.IFundServices
{
    public interface IWithdrawMoneyService
    {
        Task<IEnumerable<WithdrawMoney>> ListAsync();
        Task<WithdrawMoney> FindByIdAsync(int id);
        Task<SaveWithdrawMoneyResponse> SaveAsync(WithdrawMoney withdrawMoney);
        Task<SaveWithdrawMoneyResponse> UpdateAsync(int id, WithdrawMoney withdrawMoney);
        Task<SaveWithdrawMoneyResponse> DeleteAsync(int id);
    }
}
