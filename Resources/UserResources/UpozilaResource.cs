using ofarz_rest_api.Domain.Models.User;

namespace ofarz_rest_api.Resources.UserResources
{
    public class UpozilaResource
    {
        public int UpId { get; set; }

        public string Name { get; set; }

        public string UpozilaCode { get; set; }

        public int? DistrictId { get; set; }

        public virtual District District { get; set; }
    }
}
