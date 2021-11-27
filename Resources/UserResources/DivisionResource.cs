using ofarz_rest_api.Domain.Models.User;

namespace ofarz_rest_api.Resources.UserResources
{
    public class DivisionResource
    {
        public int DivId { get; set; }

        public string Name { get; set; }

        public string DivisionCode { get; set; }

        public int? CountryId { get; set; }

        public virtual Country Country { get; set; }
    }
}
