using ofarz_rest_api.Domain.Models.Fund;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IRepositories.IFundRepository
{
    public interface IWithdrawMoneyRepository
    {
        Task<IEnumerable<WithdrawMoney>> ListAsync();
        Task<WithdrawMoney> FindByIdAsync(int id);
        Task AddAsync(WithdrawMoney withdrawMoney);
        void Update(WithdrawMoney withdrawMoney);
        void Remove(WithdrawMoney withdrawMoney);
    }
}
