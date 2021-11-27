using ofarz_rest_api.Domain.Models.Fund;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IService.Communication.FundCommunication
{
    public class SaveSharerFundResponse : BaseResponse<SharerFund>
    {
        public SaveSharerFundResponse(SharerFund sharerFund) : base(sharerFund)
        { }

        public SaveSharerFundResponse(string message) : base(message)
        { }
    }
}
