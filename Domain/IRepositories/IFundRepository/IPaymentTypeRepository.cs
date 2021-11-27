using ofarz_rest_api.Domain.Models.Fund;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IRepositories.IFundRepository
{
    public interface IPaymentTypeRepository
    {
        Task<IEnumerable<PaymentType>> ListAsync();
        Task<PaymentType> FindByIdAsync(int id);
        PaymentType GetPaymentType(int id);
        Task AddAsync(PaymentType paymentType);
        void Update(PaymentType paymentType);
        void Remove(PaymentType paymentType);
    }
}
