namespace ofarz_rest_api.Domain.Models.User
{
    public class Market
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string MarketCode { get; set; }

        public int? UnionOrWardId { get; set; }

        public virtual UnionOrWard UnionOrWard { get; set; }
    }
}
