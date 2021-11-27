namespace ofarz_rest_api.Domain.Models.User
{
    public class Division
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string DivisionCode { get; set; }

        public int? CountryId { get; set; }

        public virtual Country Country { get; set; }
    }
}
