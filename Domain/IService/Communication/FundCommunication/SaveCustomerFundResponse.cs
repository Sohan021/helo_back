using ofarz_rest_api.Domain.Models.Fund;

namespace ofarz_rest_api.Domain.IService.Communication.FundCommunication
{
    public class SaveCustomerFundResponse : BaseResponse<ShoperFund>
    {
        public SaveCustomerFundResponse(ShoperFund customerFund) : base(customerFund)
        { }

        public SaveCustomerFundResponse(string message) : base(message)
        { }
    }
}
