using ofarz_rest_api.Domain.Models.User;
using System.ComponentModel.DataAnnotations;

namespace ofarz_rest_api.Resources.AuthResources.ProfileResources
{
    public class AppSharerProfileResource
    {
        public string CurrentUser { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string ProfilePhoto { get; set; }

        [DataType(DataType.PostalCode)]
        public int PostalCode { get; set; }

        public string Nominee_PhonNumber { get; set; }

        public string Nominee_Name { get; set; }

        public string Nominee_Relation { get; set; }

        public int? CountryId { get; set; }

        public Country Country { get; set; }

        public int? DivisionId { get; set; }

        public Division Division { get; set; }

        public int? DistrictId { get; set; }

        public District District { get; set; }

        public int? UpozilaId { get; set; }

        public Upozila Upozilla { get; set; }

        public int? UnionOrWardId { get; set; }

        public UnionOrWard UnionOrWard { get; set; }

        public int? MarketId { get; set; }

        public Market Market { get; set; }
    }
}
