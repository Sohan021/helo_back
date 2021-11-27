using ofarz_rest_api.Domain.IService.Communication.FundCommunication;
using ofarz_rest_api.Domain.Models.Fund;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IService.IFundServices
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> ListAsync();
        Task<Payment> FindByIdAsync(int id);
        Task<SavePaymentResponse> SaveAsync(Payment payment);
        Task<SavePaymentResponse> UpdateAsync(int id, Payment payment);
        Task<SavePaymentResponse> DeleteAsync(int id);
    }
}
