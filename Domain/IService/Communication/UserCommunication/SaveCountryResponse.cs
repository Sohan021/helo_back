using ofarz_rest_api.Domain.Models.User;

namespace ofarz_rest_api.Domain.IService.Communication.UserCommunication
{
    public class SaveCountryResponse : BaseResponse<Country>
    {
        public SaveCountryResponse(Country country) : base(country)
        { }

        public SaveCountryResponse(string message) : base(message)
        { }
    }
}
