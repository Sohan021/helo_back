using ofarz_rest_api.Domain.Models.User;

namespace ofarz_rest_api.Domain.IService.Communication.UserCommunication
{
    public class SaveCategoryResponse : BaseResponse<Category>
    {

        public SaveCategoryResponse(Category category) : base(category)
        { }


        public SaveCategoryResponse(string message) : base(message)
        { }
    }
}
