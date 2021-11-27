using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ofarz_rest_api.Resources.AuthResources.RegistrationResources
{
    public class CeoRegistrationResource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string MobileNumber { get; set; }

        public string Email { get; set; }

        public IFormFile profilePhotoFile { get; set; }

        public string profilePhoto { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
