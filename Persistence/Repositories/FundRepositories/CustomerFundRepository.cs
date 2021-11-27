using Microsoft.EntityFrameworkCore;
using ofarz_rest_api.Domain.IRepositories.IFundRepository;
using ofarz_rest_api.Domain.Models.Fund;
using ofarz_rest_api.Persistence.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ofarz_rest_api.Persistence.Repositories.FundRepositories
{
    public class CustomerFundRepository : BaseRepository, ICustomerFundRepository
    {
        public CustomerFundRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(ShoperFund customerFund)
        {
            await _context.ShoperFunds.AddAsync(customerFund);
        }

        public async Task<ShoperFund> FindByIdAsync(int id)
        {
            return await _context.ShoperFunds.FindAsync(id);
        }

        public async Task<IEnumerable<ShoperFund>> ListAsync()
        {
            return await _context.ShoperFunds.ToListAsync();
        }

        public void Remove(ShoperFund customerFund)
        {
            _context.ShoperFunds.Remove(customerFund);
        }

        public void Update(ShoperFund customerFund)
        {
            _context.ShoperFunds.Update(customerFund);
        }
    }
}
