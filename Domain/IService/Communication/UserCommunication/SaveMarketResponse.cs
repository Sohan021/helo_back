using ofarz_rest_api.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IService.Communication.UserCommunication
{
    public class SaveMarketResponse : BaseResponse<Market>
    {
        public SaveMarketResponse(Market market) : base(market)
        { }

        public SaveMarketResponse(string message) : base(message)
        { }
    }
}
