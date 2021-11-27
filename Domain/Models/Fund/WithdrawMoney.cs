using ofarz_rest_api.Domain.Models.Account;
using System;

namespace ofarz_rest_api.Domain.Models.Fund
{
    public class WithdrawMoney
    {
        public int Id { get; set; }

        public string CurrentUserId { get; set; }

        public double Amount { get; set; }

        public string AgentPhnNumber { get; set; }

        public string OfarzPhoneNumber { get; set; }

        public string UserName { get; set; }

        public string UserPhoneNumber { get; set; }

        public DateTime PaymentTime { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

    }
}
