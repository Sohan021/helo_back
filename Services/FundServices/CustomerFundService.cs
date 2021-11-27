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
    public class CustomerFundService : ICustomerFundService
    {
        private readonly ICustomerFundRepository _customerFundRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerFundService(ICustomerFundRepository customerFundRepository, IUnitOfWork unitOfWork)
        {
            _customerFundRepository = customerFundRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SaveCustomerFundResponse> DeleteAsync(int id)
        {
            var existingCustomerFund = await _customerFundRepository.FindByIdAsync(id);

            if (existingCustomerFund == null)
                return new SaveCustomerFundResponse("Category not found.");

            try
            {
                _customerFundRepository.Remove(existingCustomerFund);
                await _unitOfWork.CompleteAsync();

                return new SaveCustomerFundResponse(existingCustomerFund);
            }

            catch (Exception ex)
            {
                return new SaveCustomerFundResponse($"An error occurred when deleting the category: {ex.Message}");
            }
        }

        public async Task<ShoperFund> FindByIdAsync(int id)
        {
            return await _customerFundRepository.FindByIdAsync(id);
        }

        public async Task<IEnumerable<ShoperFund>> ListAsync()
        {
            return await _customerFundRepository.ListAsync();
        }

        public async Task<SaveCustomerFundResponse> SaveAsync(ShoperFund customerFund)
        {
            try
            {
                await _customerFundRepository.AddAsync(customerFund);
                await _unitOfWork.CompleteAsync();

                return new SaveCustomerFundResponse(customerFund);
            }
            catch (Exception ex)
            {
                return new SaveCustomerFundResponse($"An error occurred when saving the category: {ex.Message}");
            }
        }

        public async Task<SaveCustomerFundResponse> UpdateAsync(int id, ShoperFund customerFund)
        {
            var existingCustomerFund = await _customerFundRepository.FindByIdAsync(id);

            if (existingCustomerFund == null)
                return new SaveCustomerFundResponse("Coumntry not found");

            //existingCustomerFund.Name = country.Name;

            try
            {
                _customerFundRepository.Update(existingCustomerFund);
                await _unitOfWork.CompleteAsync();

                return new SaveCustomerFundResponse(existingCustomerFund);
            }
            catch (Exception ex)
            {
                return new SaveCustomerFundResponse($"An error occurred when updating the category: {ex.Message}");
            }
        }
    }
}
