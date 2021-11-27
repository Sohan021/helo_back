using ofarz_rest_api.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IRepositories.IUserRepositories
{
    public interface IMarketRepository
    {
        Task<IEnumerable<Market>> ListAsync();
        Task<Market> FindByIdAsync(int id);
        Market GetMarket(int id);
        Task AddAsync(Market market);
        void Update(Market market);
        void Remove(Market market);
    }
}
