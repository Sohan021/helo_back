using ofarz_rest_api.Domain.Models.Fund;

namespace ofarz_rest_api.Domain.IService.Communication.FundCommunication
{
    public class SavePaymentTypeResponse : BaseResponse<PaymentType>
    {
        public SavePaymentTypeResponse(PaymentType country) : base(country)
        { }

        public SavePaymentTypeResponse(string message) : base(message)
        { }
    }
}
