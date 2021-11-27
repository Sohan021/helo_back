using ofarz_rest_api.Domain.Models.Account;

namespace ofarz_rest_api.Domain.Models.Fund
{
    public class ShoperFund
    {
        public int Id { get; set; }

        public double BackShoppingAccount { get; set; }

        public string ShoperPhoneNumber { get; set; }

        public string ShoperName { get; set; }

        public string ShoperId { get; set; }

        public ApplicationUser Shoper { get; set; }
    }
}
