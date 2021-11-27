using Microsoft.EntityFrameworkCore;
using ofarz_rest_api.Domain.IRepositories.IFundRepository;
using ofarz_rest_api.Domain.Models.Fund;
using ofarz_rest_api.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ofarz_rest_api.Persistence.Repositories.FundRepositories
{
    public class WithdrawMoneyRepository : BaseRepository, IWithdrawMoneyRepository
    {
        public WithdrawMoneyRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(WithdrawMoney withdrawMoney)
        {
            await _context.WithdrawMoney.AddAsync(withdrawMoney);
        }

        public async Task<WithdrawMoney> FindByIdAsync(int id)
        {
            return await _context.WithdrawMoney.FindAsync(id);
        }

        public async Task<IEnumerable<WithdrawMoney>> ListAsync()
        {
            return await _context.WithdrawMoney.ToListAsync();
        }

        public void Remove(WithdrawMoney withdrawMoney)
        {
            _context.WithdrawMoney.Remove(withdrawMoney);
        }

        public void Update(WithdrawMoney withdrawMoney)
        {
            _context.WithdrawMoney.Update(withdrawMoney);
        }
    }
}
