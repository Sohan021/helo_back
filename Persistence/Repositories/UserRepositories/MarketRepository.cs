using Microsoft.EntityFrameworkCore;
using ofarz_rest_api.Domain.IRepositories.IUserRepositories;
using ofarz_rest_api.Domain.Models.User;
using ofarz_rest_api.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ofarz_rest_api.Persistence.Repositories.UserRepositories
{
    public class MarketRepository : BaseRepository, IMarketRepository
    {
        public MarketRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Market market)
        {
            await _context.Markets.AddAsync(market);
        }

        public Market GetMarket(int id)
        {
            return _context.Markets.Where(a => a.Id == id).FirstOrDefault();
        }

        public async Task<Market> FindByIdAsync(int id)
        {
            return await _context.Markets.Include(_ => _.UnionOrWard).FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Market>> ListAsync()
        {
            return await _context.Markets.Include(_ => _.UnionOrWard).ToListAsync();
        }

        public void Remove(Market market)
        {
            _context.Markets.Remove(market);
        }

        public void Update(Market market)
        {
            _context.Markets.Update(market);
        }
    }
}
