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
    public class WithdrawMoneyService : IWithdrawMoneyService
    {
        private readonly IWithdrawMoneyRepository _withdrawMoneyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public WithdrawMoneyService(IWithdrawMoneyRepository withdrawMoneyRepository, IUnitOfWork unitOfWork)
        {
            _withdrawMoneyRepository = withdrawMoneyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SaveWithdrawMoneyResponse> DeleteAsync(int id)
        {
            var existingWithdrawMoney = await _withdrawMoneyRepository.FindByIdAsync(id);

            if (existingWithdrawMoney == null)
                return new SaveWithdrawMoneyResponse("Category not found.");

            try
            {
                _withdrawMoneyRepository.Remove(existingWithdrawMoney);
                await _unitOfWork.CompleteAsync();

                return new SaveWithdrawMoneyResponse(existingWithdrawMoney);
            }

            catch (Exception ex)
            {
                return new SaveWithdrawMoneyResponse($"An error occurred when deleting the category: {ex.Message}");
            }
        }

        public async Task<WithdrawMoney> FindByIdAsync(int id)
        {
            return await _withdrawMoneyRepository.FindByIdAsync(id);
        }

        public async Task<IEnumerable<WithdrawMoney>> ListAsync()
        {
            return await _withdrawMoneyRepository.ListAsync();
        }

        public async Task<SaveWithdrawMoneyResponse> SaveAsync(WithdrawMoney withdrawMoney)
        {
            try
            {
                await _withdrawMoneyRepository.AddAsync(withdrawMoney);
                await _unitOfWork.CompleteAsync();

                return new SaveWithdrawMoneyResponse(withdrawMoney);
            }
            catch (Exception ex)
            {
                return new SaveWithdrawMoneyResponse($"An error occurred when saving the category: {ex.Message}");
            }
        }

        public async Task<SaveWithdrawMoneyResponse> UpdateAsync(int id, WithdrawMoney withdrawMoney)
        {
            var existingWithdrawMoney = await _withdrawMoneyRepository.FindByIdAsync(id);

            if (existingWithdrawMoney == null)
                return new SaveWithdrawMoneyResponse("Coumntry not found");

            //existingWithdrawMoney.Name = country.Name;

            try
            {
                _withdrawMoneyRepository.Update(existingWithdrawMoney);
                await _unitOfWork.CompleteAsync();

                return new SaveWithdrawMoneyResponse(existingWithdrawMoney);
            }
            catch (Exception ex)
            {
                return new SaveWithdrawMoneyResponse($"An error occurred when updating the category: {ex.Message}");
            }
        }
    }
}
