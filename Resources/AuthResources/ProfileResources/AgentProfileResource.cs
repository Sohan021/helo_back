using Microsoft.AspNetCore.Http;

namespace ofarz_rest_api.Resources.AuthResources.ProfileResources
{
    public class AgentProfileResource
    {
        public string CurrentUser { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MobileNumber { get; set; }

        public string Email { get; set; }

        public IFormFile ProfilePhotoFile { get; set; }

        public string ProfilePhoto { get; set; }
    }
}
