using ofarz_rest_api.Domain.Models.Account;

namespace ofarz_rest_api.Domain.Models.Fund
{
    public class SharerFund
    {
        public string Id { get; set; }

        public double MainAccount { get; set; }

        public double BackShoppingAccount { get; set; }

        public string SharerName { get; set; }

        public string SharerPhoneNumber { get; set; }

        public string SharerId { get; set; }

        public ApplicationUser Sharer { get; set; }
    }
}
