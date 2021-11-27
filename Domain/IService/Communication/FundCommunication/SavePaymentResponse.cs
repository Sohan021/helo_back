using ofarz_rest_api.Domain.Models.Fund;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IService.Communication.FundCommunication
{
    public class SavePaymentResponse : BaseResponse<Payment>
    {
        public SavePaymentResponse(Payment payment) : base(payment)
        { }

        public SavePaymentResponse(string message) : base(message)
        { }
    }
}
