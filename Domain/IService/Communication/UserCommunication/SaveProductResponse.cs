using ofarz_rest_api.Domain.Models.User;

namespace ofarz_rest_api.Domain.IService.Communication.UserCommunication
{
    public class SaveProductResponse : BaseResponse<Product>
    {
        public SaveProductResponse(Product product) : base(product) { }

        public SaveProductResponse(string message) : base(message) { }
    }
}
