using ofarz_rest_api.Domain.IRepositories;
using ofarz_rest_api.Domain.IRepositories.IUserRepositories;
using ofarz_rest_api.Domain.IService.Communication.UserCommunication;
using ofarz_rest_api.Domain.IService.IUserServices;
using ofarz_rest_api.Domain.Models.User;
using ofarz_rest_api.Resources.UserResources;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ofarz_rest_api.Services.UserServices
{
    public class DivisionService : IDivisionService
    {

        private readonly IDivisionRepository _divisionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DivisionService(IDivisionRepository divisionRepository, IUnitOfWork unitOfWork)
        {
            _divisionRepository = divisionRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<SaveDivisionResponse> DeleteAsync(int id)
        {
            var existingDivision = await _divisionRepository.FindByIdAsync(id);

            if (existingDivision == null)
                return new SaveDivisionResponse("Division Not Found");

            try
            {
                _divisionRepository.Remove(existingDivision);
                await _unitOfWork.CompleteAsync();

                return new SaveDivisionResponse(existingDivision);
            }
            catch (Exception ex)
            {
                return new SaveDivisionResponse($"An error Occured: {ex.Message}");
            }

        }

        public async Task<Division> FindByIdAsync(int id)
        {
            return await _divisionRepository.FindByIdAsync(id);
        }

        public Division GetDivision(int id)
        {
            return _divisionRepository.GetDivision(id);
        }

        public async Task<IEnumerable<Division>> ListAsync()
        {
            return await _divisionRepository.ListAsync();
        }

        public async Task<SaveDivisionResponse> SaveAsync(Division division)
        {
            try
            {
                await _divisionRepository.AddAsync(division);
                await _unitOfWork.CompleteAsync();

                return new SaveDivisionResponse(division);
            }

            catch (Exception ex)
            {
                return new SaveDivisionResponse($"An error Occured : {ex.Message}");
            }
        }

        public async Task<SaveDivisionResponse> UpdateAsync(int id, DivisionResource resource)
        {
            var existingDivision = await _divisionRepository.FindByIdAsync(id);

            if (existingDivision == null)
                return new SaveDivisionResponse("Division not found");

            existingDivision.Name = resource.Name;
            existingDivision.DivisionCode = resource.DivisionCode;
            existingDivision.CountryId = resource.CountryId;

            try
            {
                _divisionRepository.Update(existingDivision);
                await _unitOfWork.CompleteAsync();

                return new SaveDivisionResponse(existingDivision);
            }
            catch (Exception ex)
            {
                return new SaveDivisionResponse($"An error occured: {ex.Message}");
            }
        }
    }
}
