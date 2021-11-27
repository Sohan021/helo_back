using Microsoft.EntityFrameworkCore;
using ofarz_rest_api.Domain.IRepositories.IFundRepository;
using ofarz_rest_api.Domain.Models.Fund;
using ofarz_rest_api.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ofarz_rest_api.Persistence.Repositories.FundRepositories

{
    public class PaymentTypeRepository : BaseRepository, IPaymentTypeRepository
    {

        public PaymentTypeRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(PaymentType paymentType)
        {
            await _context.PaymentTypes.AddAsync(paymentType);
        }

        public async Task<PaymentType> FindByIdAsync(int id)
        {
            return await _context.PaymentTypes.FindAsync(id);
        }

        public PaymentType GetPaymentType(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PaymentType>> ListAsync()
        {
            return await _context.PaymentTypes.ToListAsync();
        }

        public void Remove(PaymentType paymentType)
        {
            _context.PaymentTypes.Remove(paymentType);
        }

        public void Update(PaymentType paymentType)
        {
            _context.PaymentTypes.Update(paymentType);
        }
    }
}
