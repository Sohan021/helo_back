using ofarz_rest_api.Domain.IService.Communication.FundCommunication;
using ofarz_rest_api.Domain.Models.Fund;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IService.IFundServices
{
    public interface IAgentFundService
    {
        Task<IEnumerable<AgentFund>> ListAsync();
        Task<AgentFund> FindByIdAsync(int id);
        Task<SaveAgentFundResponse> SaveAsync(AgentFund agentFund);
        Task<SaveAgentFundResponse> UpdateAsync(int id, AgentFund agentFund);
        Task<SaveAgentFundResponse> DeleteAsync(int id);
    }
}
