using ofarz_rest_api.Domain.IService.Communication.FundCommunication;
using ofarz_rest_api.Domain.Models.Fund;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IService.IFundServices
{
    public interface IPaymentTypeService
    {
        Task<IEnumerable<PaymentType>> ListAsync();
        Task<PaymentType> FindByIdAsync(int id);
        PaymentType GetPaymentType(int id);
        Task<SavePaymentTypeResponse> SaveAsync(PaymentType paymentType);
        Task<SavePaymentTypeResponse> UpdateAsync(int id, PaymentType paymentType);
        Task<SavePaymentTypeResponse> DeleteAsync(int id);
    }
}
