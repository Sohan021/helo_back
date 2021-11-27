using ofarz_rest_api.Domain.IRepositories;
using ofarz_rest_api.Domain.IRepositories.IUserRepositories;
using ofarz_rest_api.Domain.IService.Communication.UserCommunication;
using ofarz_rest_api.Domain.IService.IUserServices;
using ofarz_rest_api.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ofarz_rest_api.Services.UserServices
{
    public class MarketService : IMarketService
    {

        private readonly IMarketRepository _marketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MarketService(IMarketRepository marketRepository, IUnitOfWork unitOfWork)
        {
            _marketRepository = marketRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SaveMarketResponse> DeleteAsync(int id)
        {
            var existingMarket = await _marketRepository.FindByIdAsync(id);

            if (existingMarket == null)
                return new SaveMarketResponse("Market not found.");

            try
            {
                _marketRepository.Remove(existingMarket);
                await _unitOfWork.CompleteAsync();

                return new SaveMarketResponse(existingMarket);
            }

            catch (Exception ex)
            {
                return new SaveMarketResponse($"An error occurred when deleting the market: {ex.Message}");
            }
        }

        public async Task<Market> FindByIdAsync(int id)
        {
            return await _marketRepository.FindByIdAsync(id);
        }



        public Market GetMarket(int id)
        {
            return _marketRepository.GetMarket(id);
        }

        public async Task<IEnumerable<Market>> ListAsync()
        {
            return await _marketRepository.ListAsync();
        }

        public async Task<SaveMarketResponse> SaveAsync(Market market)
        {
            try
            {
                await _marketRepository.AddAsync(market);
                await _unitOfWork.CompleteAsync();

                return new SaveMarketResponse(market);
            }
            catch (Exception ex)
            {
                return new SaveMarketResponse($"An error occurred when saving the market: {ex.Message}");
            }
        }

        public async Task<SaveMarketResponse> UpdateAsync(int id, Market market)
        {
            var existingMarket = await _marketRepository.FindByIdAsync(id);

            if (existingMarket == null)
                return new SaveMarketResponse("Market not found");

            existingMarket.Name = market.Name;
            existingMarket.MarketCode = market.MarketCode;
            existingMarket.UnionOrWardId = market.UnionOrWardId;

            try
            {
                _marketRepository.Update(existingMarket);
                await _unitOfWork.CompleteAsync();

                return new SaveMarketResponse(existingMarket);
            }
            catch (Exception ex)
            {
                return new SaveMarketResponse($"An error occurred when updating the market: {ex.Message}");
            }

        }

    }
}
