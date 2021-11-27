using ofarz_rest_api.Domain.Models.Account;
using ofarz_rest_api.Domain.Models.User;
using System;

namespace ofarz_rest_api.Domain.Models.Fund
{
    public class Payment
    {
        public int Id { get; set; }

        public double Amount { get; set; }

        public string AgentPhnNumber { get; set; }

        public string PayerName { get; set; }

        public string PayerPhoneNumber { get; set; }

        public DateTime PaymentTime { get; set; }

        public string PayerId { get; set; }

        public ApplicationUser Payer { get; set; }

        public int ProductTypeId { get; set; }

        public ProductType ProductType { get; set; }

        public int PaymentTypeId { get; set; }

        public PaymentType PaymentType { get; set; }
    }
}
