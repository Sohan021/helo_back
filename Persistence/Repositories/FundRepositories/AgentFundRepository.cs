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
    public class AgentFundRepository : BaseRepository, IAgentFundRepository
    {
        public AgentFundRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(AgentFund agentFund)
        {
            await _context.AgentFunds.AddAsync(agentFund);
        }

        public async Task<AgentFund> FindByIdAsync(int id)
        {
            return await _context.AgentFunds.FindAsync(id);
        }

        public async Task<IEnumerable<AgentFund>> ListAsync()
        {
            return await _context.AgentFunds.ToListAsync();
        }

        public void Remove(AgentFund agentFund)
        {
            _context.AgentFunds.Remove(agentFund);
        }

        public void Update(AgentFund agentFund)
        {
            _context.AgentFunds.Update(agentFund);
        }
    }
}
