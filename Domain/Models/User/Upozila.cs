namespace ofarz_rest_api.Domain.Models.User
{
    public class Upozila
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string UpozilaCode { get; set; }

        public int? DistrictId { get; set; }

        public virtual District District { get; set; }

    }
}
