using ofarz_rest_api.Domain.Models.User;

namespace ofarz_rest_api.Resources.UserResources
{
    public class MarketResource
    {
        public int? MarId { get; set; }

        public string Name { get; set; }

        public string MarketCode { get; set; }

        public int? UnionOrWardId { get; set; }

        public UnionOrWard UnionOrWard { get; set; }
    }
}
