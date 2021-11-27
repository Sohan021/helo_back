using ofarz_rest_api.Domain.Models.Account;
using System;

namespace ofarz_rest_api.Resources.FundResources.Withdraw
{
    public class WithdrawMoneyCeoToOfarzResource
    {
        public int Id { get; set; }

        public string CurrentUserId { get; set; }

        public double Amount { get; set; }

        public string OfarzPhoneNumber { get; set; }

        public string CeoName { get; set; }

        public string CeoPhoneNumber { get; set; }

        public DateTime WithdrawTime { get; set; }

        public string CeoId { get; set; }

        public ApplicationUser Ceo { get; set; }
    }
}
