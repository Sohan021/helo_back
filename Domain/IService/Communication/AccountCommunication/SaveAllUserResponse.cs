using ofarz_rest_api.Domain.Models.Account;

namespace ofarz_rest_api.Domain.IService.Communication.AccountCommunication
{
    public class SaveAllUserResponse : BaseResponse<ApplicationUser>
    {
        public SaveAllUserResponse(ApplicationUser applicationUser) : base(applicationUser)
        { }


        public SaveAllUserResponse(string message) : base(message)
        { }
    }
}
