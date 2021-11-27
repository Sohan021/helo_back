using ofarz_rest_api.Domain.Models.Fund;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IService.Communication.FundCommunication
{
    public class SaveAgentFundResponse : BaseResponse<AgentFund>
    {
        public SaveAgentFundResponse(AgentFund agentFund) : base(agentFund)
        { }

        public SaveAgentFundResponse(string message) : base(message)
        { }
    }
}
