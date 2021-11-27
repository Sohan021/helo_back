using System.ComponentModel.DataAnnotations;

namespace ofarz_rest_api.Resources.AuthResources.RegistrationResources
{
    public class AppSharerRegistrationResource
    {
        public string CurrentUser { get; set; }

        public string MobileNumber { get; set; }

        public string NID_Number { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
