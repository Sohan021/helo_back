using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ofarz_rest_api.Resources.AuthResources.RegistrationResources
{
    public class KarrotRegistrationResource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string MobileNumber { get; set; }

        public string Email { get; set; }

        public IFormFile KarrotLogoFile { get; set; }

        public string KarrotLogo { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }


    }
}
