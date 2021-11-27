using ofarz_rest_api.Domain.Models.Account;
using System;

namespace ofarz_rest_api.Resources.FundResources.Withdraw
{
    public class WithdrawMoneyAgentToOfarzResource
    {
        public int Id { get; set; }

        public string CurrentUserId { get; set; }

        public double Amount { get; set; }

        public string OfarzPhoneNumber { get; set; }

        public string AgentName { get; set; }

        public string AgentPhoneNumber { get; set; }

        public DateTime WithdrawTime { get; set; }

        public string AgentId { get; set; }

        public ApplicationUser Agent { get; set; }
    }
}
