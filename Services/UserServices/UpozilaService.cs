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
    public class UpozilaService : IUpozillaService
    {
        private readonly IUpozillaRepository _upozillaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpozilaService(IUpozillaRepository upozillaRepository, IUnitOfWork unitOfWork)
        {
            _upozillaRepository = upozillaRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SaveUpozillaResponse> DeleteAsync(int id)
        {
            var existingUpozilla = await _upozillaRepository.FindByIdAsync(id);

            if (existingUpozilla == null)
                return new SaveUpozillaResponse("An error Occured");

            try
            {
                _upozillaRepository.Remove(existingUpozilla);
                await _unitOfWork.CompleteAsync();

                return new SaveUpozillaResponse(existingUpozilla);
            }
            catch (Exception ex)
            {
                return new SaveUpozillaResponse($"An error occured: {ex.Message}");
            }
        }

        public async Task<Upozila> FindByIdAsync(int id)
        {
            return await _upozillaRepository.FindByIdAsync(id);
        }

        public Upozila GetUpozilla(int id)
        {
            return _upozillaRepository.GetUpozilla(id);
        }

        public async Task<IEnumerable<Upozila>> ListAsync()
        {
            return await _upozillaRepository.ListAsync();
        }

        public async Task<SaveUpozillaResponse> SaveAsync(Upozila upozilla)
        {
            try
            {
                await _upozillaRepository.AddAsync(upozilla);
                await _unitOfWork.CompleteAsync();

                return new SaveUpozillaResponse(upozilla);
            }
            catch (Exception ex)
            {
                return new SaveUpozillaResponse($"An error occured: {ex.Message}");
            }
        }

        public async Task<SaveUpozillaResponse> UpdateAsync(int id, UpozilaResource resource)
        {
            var existingUpozilla = await _upozillaRepository.FindByIdAsync(id);

            if (existingUpozilla == null)
                return new SaveUpozillaResponse("An error occured");

            existingUpozilla.Name = resource.Name;
            existingUpozilla.UpozilaCode = resource.UpozilaCode;
            existingUpozilla.DistrictId = resource.DistrictId;

            try
            {
                _upozillaRepository.Update(existingUpozilla);
                await _unitOfWork.CompleteAsync();

                return new SaveUpozillaResponse(existingUpozilla);
            }
            catch (Exception ex)
            {
                return new SaveUpozillaResponse($"An error occured: {ex.Message}");
            }
        }
    }
}
