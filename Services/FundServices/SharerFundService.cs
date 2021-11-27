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
    public class SharerFundService : ISharerFundService
    {
        private readonly ISharerFundRepository _sharerFundRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SharerFundService(ISharerFundRepository sharerFundRepository, IUnitOfWork unitOfWork)
        {
            _sharerFundRepository = sharerFundRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SaveSharerFundResponse> DeleteAsync(int id)
        {
            var existingSharerFund = await _sharerFundRepository.FindByIdAsync(id);

            if (existingSharerFund == null)
                return new SaveSharerFundResponse("Category not found.");

            try
            {
                _sharerFundRepository.Remove(existingSharerFund);
                await _unitOfWork.CompleteAsync();

                return new SaveSharerFundResponse(existingSharerFund);
            }

            catch (Exception ex)
            {
                return new SaveSharerFundResponse($"An error occurred when deleting the category: {ex.Message}");
            }
        }

        public async Task<SharerFund> FindByIdAsync(int id)
        {
            return await _sharerFundRepository.FindByIdAsync(id);
        }

        public async Task<IEnumerable<SharerFund>> ListAsync()
        {
            return await _sharerFundRepository.ListAsync();
        }

        public async Task<SaveSharerFundResponse> SaveAsync(SharerFund sharerFund)
        {
            try
            {
                await _sharerFundRepository.AddAsync(sharerFund);
                await _unitOfWork.CompleteAsync();

                return new SaveSharerFundResponse(sharerFund);
            }
            catch (Exception ex)
            {
                return new SaveSharerFundResponse($"An error occurred when saving the category: {ex.Message}");
            }
        }

        public async Task<SaveSharerFundResponse> UpdateAsync(int id, SharerFund sharerFund)
        {
            var existingSharerFund = await _sharerFundRepository.FindByIdAsync(id);

            if (existingSharerFund == null)
                return new SaveSharerFundResponse("Coumntry not found");


            try
            {
                _sharerFundRepository.Update(existingSharerFund);
                await _unitOfWork.CompleteAsync();

                return new SaveSharerFundResponse(existingSharerFund);
            }
            catch (Exception ex)
            {
                return new SaveSharerFundResponse($"An error occurred when updating the category: {ex.Message}");
            }
        }
    }
}
