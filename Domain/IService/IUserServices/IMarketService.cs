using ofarz_rest_api.Domain.IService.Communication.UserCommunication;
using ofarz_rest_api.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IService.IUserServices
{
    public interface IMarketService
    {
        Task<IEnumerable<Market>> ListAsync();
        Task<Market> FindByIdAsync(int id);
        Market GetMarket(int id);
        Task<SaveMarketResponse> SaveAsync(Market market);
        Task<SaveMarketResponse> UpdateAsync(int id, Market country);
        Task<SaveMarketResponse> DeleteAsync(int id);
    }
}
