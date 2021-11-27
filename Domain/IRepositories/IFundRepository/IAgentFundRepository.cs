using ofarz_rest_api.Domain.Models.Fund;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IRepositories.IFundRepository
{
    public interface IAgentFundRepository
    {
        Task<IEnumerable<AgentFund>> ListAsync();
        Task<AgentFund> FindByIdAsync(int id);
        Task AddAsync(AgentFund agentFund);
        void Update(AgentFund agentFund);
        void Remove(AgentFund agentFund);
    }
}
