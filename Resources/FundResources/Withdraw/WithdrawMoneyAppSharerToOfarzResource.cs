using ofarz_rest_api.Domain.Models.Account;
using System;

namespace ofarz_rest_api.Resources.FundResources.Withdraw
{
    public class WithdrawMoneyAppSharerToOfarzResource
    {
        public int Id { get; set; }

        public string CurrentUserId { get; set; }

        public double Amount { get; set; }

        public string OfarzPhnNumber { get; set; }

        public string SharerName { get; set; }

        public string SharerPhoneNumber { get; set; }

        public DateTime PaymentTime { get; set; }

        public string SharerId { get; set; }

        public ApplicationUser Sharer { get; set; }
    }
}
