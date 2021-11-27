using ofarz_rest_api.Domain.Models.Account;
using System;
using System.Collections.Generic;

namespace ofarz_rest_api.Domain.Models.User
{
    public class Order
    {

        public Order()
        {
            OrderDetails = new List<OrderDetail>();
            AgentOrderDetails = new List<AgentOrderDetails>();
        }

        public int Id { get; set; }

        public int OrderNo { get; set; }

        public string UserName { get; set; }

        public DateTime OrderDate { get; set; }

        public string PhoneNo { get; set; }

        public double TotalAmount { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string RoleId { get; set; }

        public ApplicationRole Role { get; set; }

        public int AddressId { get; set; }

        public Market Address { get; set; }

        public virtual List<OrderDetail> OrderDetails { get; set; }
        public virtual List<AgentOrderDetails> AgentOrderDetails { get; set; }
    }
}
