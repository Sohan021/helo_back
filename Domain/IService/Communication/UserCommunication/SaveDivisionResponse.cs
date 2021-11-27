using ofarz_rest_api.Domain.Models.User;

namespace ofarz_rest_api.Domain.IService.Communication.UserCommunication
{
    public class SaveDivisionResponse : BaseResponse<Division>
    {
        public SaveDivisionResponse(Division division) : base(division)
        { }

        public SaveDivisionResponse(string message) : base(message)
        { }
    }
}
