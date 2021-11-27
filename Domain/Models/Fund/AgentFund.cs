using ofarz_rest_api.Domain.Models.Account;

namespace ofarz_rest_api.Domain.Models.Fund
{
    public class AgentFund
    {
        public int Id { get; set; }

        public double MainAccount { get; set; }

        public double TotalTransection { get; set; }

        public double SellViaDirectCash { get; set; }

        public double ShoperTransection { get; set; }

        public string AgentName { get; set; }

        public string AgentPhoneNumber { get; set; }

        public string AgentId { get; set; }

        public ApplicationUser Agent { get; set; }
    }
}
