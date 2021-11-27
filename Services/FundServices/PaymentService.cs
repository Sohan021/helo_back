using ofarz_rest_api.Domain.IRepositories;
using ofarz_rest_api.Domain.IRepositories.IFundRepository;
using ofarz_rest_api.Domain.IService.Communication.FundCommunication;
using ofarz_rest_api.Domain.IService.IFundServices;
using ofarz_rest_api.Domain.Models.Fund;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ofarz_rest_api.Services.FundServices
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IPaymentRepository paymentRepository, IUnitOfWork unitOfWork)
        {
            _paymentRepository = paymentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SavePaymentResponse> DeleteAsync(int id)
        {
            var existingPayment = await _paymentRepository.FindByIdAsync(id);

            if (existingPayment == null)
                return new SavePaymentResponse("Category not found.");

            try
            {
                _paymentRepository.Remove(existingPayment);
                await _unitOfWork.CompleteAsync();

                return new SavePaymentResponse(existingPayment);
            }

            catch (Exception ex)
            {
                return new SavePaymentResponse($"An error occurred when deleting the category: {ex.Message}");
            }
        }

        public async Task<Payment> FindByIdAsync(int id)
        {
            return await _paymentRepository.FindByIdAsync(id);
        }

        public async Task<IEnumerable<Payment>> ListAsync()
        {
            return await _paymentRepository.ListAsync();
        }

        public async Task<SavePaymentResponse> SaveAsync(Payment payment)
        {
            try
            {
                await _paymentRepository.AddAsync(payment);
                await _unitOfWork.CompleteAsync();

                return new SavePaymentResponse(payment);
            }
            catch (Exception ex)
            {
                return new SavePaymentResponse($"An error occurred when saving the category: {ex.Message}");
            }
        }

        public async Task<SavePaymentResponse> UpdateAsync(int id, Payment payment)
        {
            var existingPayment = await _paymentRepository.FindByIdAsync(id);

            if (existingPayment == null)
                return new SavePaymentResponse("Coumntry not found");

            //existingPayment.Name = country.Name;

            try
            {
                _paymentRepository.Update(existingPayment);
                await _unitOfWork.CompleteAsync();

                return new SavePaymentResponse(existingPayment);
            }
            catch (Exception ex)
            {
                return new SavePaymentResponse($"An error occurred when updating the category: {ex.Message}");
            }
        }
    }
}
