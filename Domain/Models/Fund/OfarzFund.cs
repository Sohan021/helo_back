using ofarz_rest_api.Domain.Models.Account;

namespace ofarz_rest_api.Domain.Models.Fund
{
    public class OfarzFund
    {
        public string Id { get; set; }

        public double MainAccount { get; set; }

        public double GetMoneyByAgent { get; set; }

        public double GetMoneyByKarrot { get; set; }

        public double GetMoneyByCeo { get; set; }

        public double GetMoneyByAppSharer { get; set; }

        public double GetMoneyByAgentShopping { get; set; }

        public string MobileNumber { get; set; }

        public string OfarzId { get; set; }

        public ApplicationUser Ofarz { get; set; }
    }
}
