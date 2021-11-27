using ofarz_rest_api.Domain.Models.User;

namespace ofarz_rest_api.Domain.IService.Communication.UserCommunication
{
    public class SaveDistrictResponse : BaseResponse<District>
    {
        public SaveDistrictResponse(District district) : base(district)
        { }

        public SaveDistrictResponse(string message) : base(message)
        { }
    }
}
