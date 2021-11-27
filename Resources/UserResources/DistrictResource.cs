using ofarz_rest_api.Domain.Models.User;

namespace ofarz_rest_api.Resources.UserResources
{
    public class DistrictResource
    {
        public int DisId { get; set; }

        public string Name { get; set; }

        public string DistrictCode { get; set; }

        public int? DivisionId { get; set; }

        public virtual Division Division { get; set; }
    }
}
