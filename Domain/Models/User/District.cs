namespace ofarz_rest_api.Domain.Models.User
{
    public class District
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string DistrictCode { get; set; }

        public int? DivisionId { get; set; }

        public virtual Division Division { get; set; }
    }
}
