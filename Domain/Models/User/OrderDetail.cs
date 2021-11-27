using System.ComponentModel.DataAnnotations.Schema;

namespace ofarz_rest_api.Domain.Models.User
{
    public class OrderDetail
    {
        public int Id { get; set; }


        public int? OrderId { get; set; }

        public virtual Order Order { get; set; }


        public int? AgentOrderId { get; set; }

        public virtual AgentOrder AgentOrder { get; set; }


        public int? ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

    }
}
