using ofarz_rest_api.Domain.Models.Account;
using System;

namespace ofarz_rest_api.Resources.FundResources.Withdraw
{
    public class WithdrawMoneyKarrotToAgentResource
    {
        public int Id { get; set; }

        public string CurrentUserId { get; set; }

        public double Amount { get; set; }

        public string AgentPhoneNumber { get; set; }

        public string KarrotName { get; set; }

        public string KarrotNumber { get; set; }

        public DateTime WithdrawTime { get; set; }

        public string KarrotId { get; set; }

        public ApplicationUser Karrot { get; set; }
    }
}
