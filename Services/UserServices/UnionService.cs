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
    public class UnionService : IUnionService
    {
        private readonly IUnionOrWardRepository _unionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UnionService(IUnionOrWardRepository unionRepository, IUnitOfWork unitOfWork)
        {
            _unionRepository = unionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SaveUnionOrWardResponse> DeleteAsync(int id)
        {
            var existingUnion = await _unionRepository.FindByIdAsync(id);

            if (existingUnion == null)
                return new SaveUnionOrWardResponse("An error occured");

            try
            {
                _unionRepository.Remove(existingUnion);
                await _unitOfWork.CompleteAsync();

                return new SaveUnionOrWardResponse(existingUnion);
            }
            catch (Exception ex)
            {
                return new SaveUnionOrWardResponse($"An error occured when delete Union :{ex.Message}");
            }
        }

        public async Task<UnionOrWard> FindByIdAsync(int id)
        {
            return await _unionRepository.FindByIdAsync(id);
        }


        public async Task<IEnumerable<UnionOrWard>> ListAsync()
        {
            return await _unionRepository.ListAsync();
        }

        public async Task<SaveUnionOrWardResponse> SaveAsync(UnionOrWard union)
        {


            try
            {
                await _unionRepository.AddAsync(union);
                await _unitOfWork.CompleteAsync();

                return new SaveUnionOrWardResponse(union);
            }

            catch (Exception ex)
            {
                return new SaveUnionOrWardResponse($"An error occured: {ex.Message}");
            }
        }

        public async Task<SaveUnionOrWardResponse> UpdateAsync(int id, UnionOrWardResource resource)
        {
            var existingUnion = await _unionRepository.FindByIdAsync(id);

            if (existingUnion == null)
                return new SaveUnionOrWardResponse("An error Occured");

            existingUnion.Name = resource.Name;
            existingUnion.UnionOrWardCode = resource.UnionOrWardCode;
            existingUnion.UpozilaId = resource.UpozilaId;

            try
            {
                _unionRepository.Update(existingUnion);
                await _unitOfWork.CompleteAsync();

                return new SaveUnionOrWardResponse(existingUnion);
            }
            catch (Exception ex)
            {
                return new SaveUnionOrWardResponse($"An error occured: {ex.Message}");
            }

        }
    }
}
