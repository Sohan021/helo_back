using ofarz_rest_api.Domain.Models.Account;

namespace ofarz_rest_api.Domain.Models.Fund
{
    public class KarrotFund
    {
        public string Id { get; set; }

        public double MainAccount { get; set; }

        public double TotalIncome { get; set; }

        public int KarrotCode { get; set; }

        public string KarrotId { get; set; }

        public ApplicationUser Karrot { get; set; }
    }
}
