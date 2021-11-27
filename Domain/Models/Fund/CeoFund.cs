using ofarz_rest_api.Domain.Models.Account;

namespace ofarz_rest_api.Domain.Models.Fund
{
    public class CeoFund
    {
        public string Id { get; set; }

        public double MainAccount { get; set; }

        public double TotalIncome { get; set; }

        public int CeoCode { get; set; }

        public string CeoId { get; set; }

        public ApplicationUser Ceo { get; set; }
    }
}
