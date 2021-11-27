using ofarz_rest_api.Domain.Models.User;

namespace ofarz_rest_api.Resources.UserResources
{
    public class UnionOrWardResource
    {
        public int UnId { get; set; }

        public string Name { get; set; }

        public string UnionOrWardCode { get; set; }

        public int? UpozilaId { get; set; }

        public virtual Upozila Upozila { get; set; }
    }
}
