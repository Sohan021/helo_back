using ofarz_rest_api.Domain.Models.User;

namespace ofarz_rest_api.Domain.IService.Communication.UserCommunication
{
    public class SaveUpozillaResponse : BaseResponse<Upozila>
    {
        public SaveUpozillaResponse(Upozila upozilla) : base(upozilla)
        { }

        public SaveUpozillaResponse(string message) : base(message)
        { }
    }
}
