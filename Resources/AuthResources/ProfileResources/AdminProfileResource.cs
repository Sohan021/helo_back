using Microsoft.AspNetCore.Http;
using ofarz_rest_api.Domain.Models.User;
using System.ComponentModel.DataAnnotations;

namespace ofarz_rest_api.Resources.AuthResources.ProfileResources
{
    public class AdminProfileResource
    {
        public string CurrentUser { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MobileNumber { get; set; }

        public string Email { get; set; }

        public IFormFile ProfilePhotoFile { get; set; }

        public string ProfilePhoto { get; set; }

        [DataType(DataType.PostalCode)]
        public int PostalCode { get; set; }

        public int? CountryId { get; set; }

        public Country Country { get; set; }


        public int? DivisionId { get; set; }

        public Division Division { get; set; }


        public int? DistrictId { get; set; }

        public District District { get; set; }


        public int? UpozilaId { get; set; }

        public Upozila Upozila { get; set; }


        public int? UnionOrWardId { get; set; }

        public UnionOrWard UnionOrWard { get; set; }
    }
}
