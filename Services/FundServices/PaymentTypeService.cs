using ofarz_rest_api.Domain.IRepositories;
using ofarz_rest_api.Domain.IRepositories.IFundRepository;
using ofarz_rest_api.Domain.IService.Communication.FundCommunication;
using ofarz_rest_api.Domain.IService.IFundServices;
using ofarz_rest_api.Domain.Models.Fund;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ofarz_rest_api.Services.FundServices
{
    public class PaymentTypeService : IPaymentTypeService
    {
        private readonly IPaymentTypeRepository _paymentTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentTypeService(IPaymentTypeRepository paymentTypeRepository, IUnitOfWork unitOfWork)
        {
            _paymentTypeRepository = paymentTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SavePaymentTypeResponse> DeleteAsync(int id)
        {
            var existingType = await _paymentTypeRepository.FindByIdAsync(id);

            if (existingType == null)
                return new SavePaymentTypeResponse("Type not found.");

            try
            {
                _paymentTypeRepository.Remove(existingType);
                await _unitOfWork.CompleteAsync();

                return new SavePaymentTypeResponse(existingType);
            }

            catch (Exception ex)
            {
                return new SavePaymentTypeResponse($"An error occurred when deleting the category: {ex.Message}");
            }
        }

        public async Task<PaymentType> FindByIdAsync(int id)
        {
            return await _paymentTypeRepository.FindByIdAsync(id);
        }

        public PaymentType GetPaymentType(int id)
        {
            return _paymentTypeRepository.GetPaymentType(id);
        }

        public async Task<IEnumerable<PaymentType>> ListAsync()
        {
            return await _paymentTypeRepository.ListAsync();
        }

        public async Task<SavePaymentTypeResponse> SaveAsync(PaymentType paymentType)
        {
            try
            {
                await _paymentTypeRepository.AddAsync(paymentType);
                await _unitOfWork.CompleteAsync();

                return new SavePaymentTypeResponse(paymentType);
            }
            catch (Exception ex)
            {
                return new SavePaymentTypeResponse($"An error occurred when saving the category: {ex.Message}");
            }
        }

        public async Task<SavePaymentTypeResponse> UpdateAsync(int id, PaymentType paymentType)
        {
            var existingType = await _paymentTypeRepository.FindByIdAsync(id);

            if (existingType == null)
                return new SavePaymentTypeResponse("Type not found");

            existingType.PaymentTypeName = paymentType.PaymentTypeName;
            existingType.PaymentTypeDescription = paymentType.PaymentTypeDescription;

            try
            {
                _paymentTypeRepository.Update(existingType);
                await _unitOfWork.CompleteAsync();

                return new SavePaymentTypeResponse(existingType);
            }
            catch (Exception ex)
            {
                return new SavePaymentTypeResponse($"An error occurred when updating the Payment Type : {ex.Message}");
            }

        }

    }
}
