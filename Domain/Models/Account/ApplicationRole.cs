using Microsoft.AspNetCore.Identity;

namespace ofarz_rest_api.Domain.Models.Account
{
    public class ApplicationRole : IdentityRole
    {
        public string RoleName { get; set; }

        public string Description { get; set; }
    }
}
