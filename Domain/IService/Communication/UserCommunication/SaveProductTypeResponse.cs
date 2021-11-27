using ofarz_rest_api.Domain.Models.User;

namespace ofarz_rest_api.Domain.IService.Communication.UserCommunication
{
    public class SaveProductTypeResponse : BaseResponse<ProductType>
    {
        public SaveProductTypeResponse(ProductType productType) : base(productType) { }

        public SaveProductTypeResponse(string message) : base(message) { }
    }
}
