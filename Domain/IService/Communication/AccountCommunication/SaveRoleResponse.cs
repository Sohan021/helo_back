using ofarz_rest_api.Domain.Models.Account;

namespace ofarz_rest_api.Domain.IService.Communication.AccountCommunication
{
    public class SaveRoleResponse : BaseResponse<ApplicationRole>
    {
        public SaveRoleResponse(ApplicationRole applicationRole) : base(applicationRole)
        { }


        public SaveRoleResponse(string message) : base(message)
        { }
    }
}
