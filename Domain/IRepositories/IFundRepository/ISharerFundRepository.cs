using ofarz_rest_api.Domain.Models.Fund;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IRepositories.IFundRepository
{
    public interface ISharerFundRepository
    {
        Task<IEnumerable<SharerFund>> ListAsync();
        Task<SharerFund> FindByIdAsync(int id);
        Task AddAsync(SharerFund sharerFund);
        void Update(SharerFund sharerFund);
        void Remove(SharerFund sharerFund);
    }
}
