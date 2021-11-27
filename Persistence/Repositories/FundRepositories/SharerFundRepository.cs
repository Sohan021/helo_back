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
    public class SharerFundRepository : BaseRepository, ISharerFundRepository
    {
        public SharerFundRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(SharerFund sharerFund)
        {
            await _context.SharerFunds.AddAsync(sharerFund);
        }

        public async Task<SharerFund> FindByIdAsync(int id)
        {
            return await _context.SharerFunds.FindAsync(id);
        }

        public async Task<IEnumerable<SharerFund>> ListAsync()
        {
            return await _context.SharerFunds.ToListAsync();
        }

        public void Remove(SharerFund sharerFund)
        {
            _context.SharerFunds.Remove(sharerFund);
        }

        public void Update(SharerFund sharerFund)
        {
            _context.SharerFunds.Update(sharerFund);
        }
    }
}
