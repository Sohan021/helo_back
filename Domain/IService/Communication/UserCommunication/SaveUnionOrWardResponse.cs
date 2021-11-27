using ofarz_rest_api.Domain.Models.User;

namespace ofarz_rest_api.Domain.IService.Communication.UserCommunication
{
    public class SaveUnionOrWardResponse : BaseResponse<UnionOrWard>
    {
        public SaveUnionOrWardResponse(UnionOrWard unionOrWard) : base(unionOrWard)
        { }

        public SaveUnionOrWardResponse(string message) : base(message)
        { }
    }
}
