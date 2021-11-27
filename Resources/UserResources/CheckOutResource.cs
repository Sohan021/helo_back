using ofarz_rest_api.Domain.Models.User;
using System.Collections.Generic;

namespace ofarz_rest_api.Resources.UserResources
{
    public class CheckOutResource
    {
        public CheckOutResource()
        {
            Products = new List<Product>();
        }

        public string CurrentUserId { get; set; }

        public double Amount { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}
