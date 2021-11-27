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
    public class DistrictService : IDistrictService
    {
        private readonly IDistrictRepository _districtRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DistrictService(IDistrictRepository districtRepository, IUnitOfWork unitOfWork)
        {
            _districtRepository = districtRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SaveDistrictResponse> DeleteAsync(int id)
        {
            var existingDistrict = await _districtRepository.FindByIdAsync(id);

            if (existingDistrict == null)
                return new SaveDistrictResponse(existingDistrict);

            try
            {
                _districtRepository.Remove(existingDistrict);
                await _unitOfWork.CompleteAsync();

                return new SaveDistrictResponse("District Not found");
            }
            catch (Exception ex)
            {
                return new SaveDistrictResponse($"An error occured: {ex.Message}");
            }
        }

        public async Task<District> FindByIdAsync(int id)
        {
            return await _districtRepository.FindByIdAsync(id);
        }

        public District GetDistrict(int id)
        {
            return _districtRepository.GetDistrict(id);
        }

        public async Task<IEnumerable<District>> ListAsync()
        {
            return await _districtRepository.ListAsync();
        }

        public async Task<SaveDistrictResponse> SaveAsync(District district)
        {
            try
            {
                await _districtRepository.AddAsync(district);
                await _unitOfWork.CompleteAsync();

                return new SaveDistrictResponse(district);
            }
            catch (Exception ex)
            {
                return new SaveDistrictResponse($"An error occured: {ex.Message}");
            }
        }

        public async Task<SaveDistrictResponse> UpdateAsync(int id, DistrictResource resource)
        {
            var existingDistrict = await _districtRepository.FindByIdAsync(id);

            if (existingDistrict == null)
                return new SaveDistrictResponse("District not found");

            existingDistrict.Name = resource.Name;
            existingDistrict.DistrictCode = resource.DistrictCode;
            existingDistrict.DivisionId = resource.DivisionId;

            try
            {
                _districtRepository.Update(existingDistrict);
                await _unitOfWork.CompleteAsync();

                return new SaveDistrictResponse(existingDistrict);
            }
            catch (Exception ex)
            {
                return new SaveDistrictResponse($"An error occured: {ex.Message}");
            }
        }
    }
}
