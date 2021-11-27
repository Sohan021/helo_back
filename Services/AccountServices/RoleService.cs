using ofarz_rest_api.Domain.IRepositories;
using ofarz_rest_api.Domain.IRepositories.IAccountRepositories;
using ofarz_rest_api.Domain.IService.Communication.AccountCommunication;
using ofarz_rest_api.Domain.IService.IAccountServices;
using ofarz_rest_api.Domain.Models.Account;
using ofarz_rest_api.Resources.AuthResources.RoleResources;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ofarz_rest_api.Services.AccountServices
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SaveRoleResponse> DeleteAsync(string id)
        {
            var existingRole = await _roleRepository.FindByIdAsync(id);

            if (existingRole == null)
                return new SaveRoleResponse("Role Not Found");

            try
            {
                _roleRepository.RemoveRole(existingRole);
                await _unitOfWork.CompleteAsync();
                return new SaveRoleResponse(existingRole);
            }
            catch (Exception ex)
            {
                return new SaveRoleResponse($"An error occurred when deleting the role: { ex.Message }");
            }
        }

        public async Task<ApplicationRole> FindByIdAsync(string id)
        {
            return await _roleRepository.FindByIdAsync(id);
        }

        public async Task<IEnumerable<ApplicationRole>> ListAsync()
        {
            return await _roleRepository.ListOfRoles();
        }

        public async Task<SaveRoleResponse> SaveAsync(ApplicationRole applicationRole)
        {
            try
            {
                await _roleRepository.AddRole(applicationRole);
                await _unitOfWork.CompleteAsync();

                return new SaveRoleResponse(applicationRole);
            }
            catch (Exception ex)
            {
                return new SaveRoleResponse($"An error occurd when saving the role: {ex.Message}");
            }
        }

        public async Task<SaveRoleResponse> UpdateAsync(string id, RoleResource roleResource)
        {
            var existingRole = await _roleRepository.FindByIdAsync(id);

            if (existingRole == null)
                return new SaveRoleResponse("Role Not Found");

            existingRole.Name = roleResource.Name;
            existingRole.RoleName = roleResource.RoleName;
            existingRole.Description = roleResource.Description;

            try
            {
                _roleRepository.UpdateRole(existingRole);
                await _unitOfWork.CompleteAsync();

                return new SaveRoleResponse(existingRole);
            }
            catch (Exception ex)
            {
                return new SaveRoleResponse($"An error occurred when updating the role: {ex.Message}");
            }

        }
    }
}
