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
    public class CountryService : ICountryService
    {

        private readonly ICountryRepository _countryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CountryService(ICountryRepository countryRepository, IUnitOfWork unitOfWork)
        {
            _countryRepository = countryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SaveCountryResponse> DeleteAsync(int id)
        {
            var existingCountry = await _countryRepository.FindByIdAsync(id);

            if (existingCountry == null)
                return new SaveCountryResponse("Category not found.");

            try
            {
                _countryRepository.Remove(existingCountry);
                await _unitOfWork.CompleteAsync();

                return new SaveCountryResponse(existingCountry);
            }

            catch (Exception ex)
            {
                return new SaveCountryResponse($"An error occurred when deleting the category: {ex.Message}");
            }
        }

        public async Task<Country> FindByIdAsync(int id)
        {
            return await _countryRepository.FindByIdAsync(id);
        }

        public Country GetCountry(int id)
        {
            return _countryRepository.GetCountry(id);
        }

        public async Task<IEnumerable<Country>> ListAsync()
        {
            return await _countryRepository.ListAsync();
        }

        public async Task<SaveCountryResponse> SaveAsync(Country country)
        {
            try
            {
                await _countryRepository.AddAsync(country);
                await _unitOfWork.CompleteAsync();

                return new SaveCountryResponse(country);
            }
            catch (Exception ex)
            {
                return new SaveCountryResponse($"An error occurred when saving the category: {ex.Message}");
            }
        }

        public async Task<SaveCountryResponse> UpdateAsync(int id, Country country)
        {
            var existingCountry = await _countryRepository.FindByIdAsync(id);

            if (existingCountry == null)
                return new SaveCountryResponse("Coumntry not found");

            existingCountry.Name = country.Name;
            existingCountry.CountryCode = country.CountryCode;

            try
            {
                _countryRepository.Update(existingCountry);
                await _unitOfWork.CompleteAsync();

                return new SaveCountryResponse(existingCountry);
            }
            catch (Exception ex)
            {
                return new SaveCountryResponse($"An error occurred when updating the category: {ex.Message}");
            }

        }

    }
}
