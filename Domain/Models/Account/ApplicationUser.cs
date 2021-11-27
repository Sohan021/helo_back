using Microsoft.AspNetCore.Identity;
using ofarz_rest_api.Domain.Models.User;
using System.ComponentModel.DataAnnotations;

namespace ofarz_rest_api.Domain.Models.Account
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MobileNumber { get; set; }

        public string NID_Number { get; set; }

        public string ProfilePhoto { get; set; }

        [DataType(DataType.PostalCode)]
        public int PostalCode { get; set; }

        public string Nominee_PhonNumber { get; set; }

        public string Nominee_Name { get; set; }

        public string Nominee_Relation { get; set; }

        public int DownlineCount { get; set; }

        public int FirstLevelDownlineCount { get; set; }

        public int SecondLevelDownlineCount { get; set; }

        public int ThirdLevelDownlineCount { get; set; }

        public int FourthLevelDownlineCount { get; set; }

        public int FifthLevelDownlineCount { get; set; }

        public string AgentShopName { get; set; }

        public int AgentCode { get; set; }

        public string ReffrerName { get; set; }

        public string ReffrerId { get; set; }

        public ApplicationUser Reffrer { get; set; }

        public string RefferName { get; set; }


        public string ApplicationRoleId { get; set; }

        public ApplicationRole ApplicationRole { get; set; }


        public int? CountryId { get; set; }

        public Country Country { get; set; }

        public string CountryName { get; set; }


        public int? DivisionId { get; set; }

        public Division Division { get; set; }

        public string DivisionName { get; set; }



        public int? DistrictId { get; set; }

        public District District { get; set; }

        public string DistrictName { get; set; }



        public int? UpozilaId { get; set; }

        public Upozila Upozilla { get; set; }

        public string UpozilaName { get; set; }



        public int? UnionOrWardId { get; set; }

        public UnionOrWard UnionOrWard { get; set; }

        public string UnionOrWardName { get; set; }



        public int? MarketId { get; set; }

        public Market Market { get; set; }

        public string MarketName { get; set; }

        public string MarketCode { get; set; }

    }
}
