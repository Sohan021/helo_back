using ofarz_rest_api.Domain.Models.Fund;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IRepositories.IFundRepository
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> ListAsync();
        Task<Payment> FindByIdAsync(int id);
        Task AddAsync(Payment payment);
        void Update(Payment payment);
        void Remove(Payment payment);
    }
}
