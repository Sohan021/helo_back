using System.ComponentModel.DataAnnotations;

namespace ofarz_rest_api.Resources.AuthResources.RegistrationResources
{
    public class ShoperRegistrationResource
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MobileNumber { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
