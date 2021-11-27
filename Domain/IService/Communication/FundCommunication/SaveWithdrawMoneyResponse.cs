using ofarz_rest_api.Domain.Models.Fund;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IService.Communication.FundCommunication
{
    public class SaveWithdrawMoneyResponse : BaseResponse<WithdrawMoney>
    {
        public SaveWithdrawMoneyResponse(WithdrawMoney withdrawMoney) : base(withdrawMoney)
        { }

        public SaveWithdrawMoneyResponse(string message) : base(message)
        { }
    }
}
