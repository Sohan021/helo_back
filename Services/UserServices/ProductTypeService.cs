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
    public class ProductTypeService : IProductTypeService
    {
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductTypeService(IProductTypeRepository productTypeRepository, IUnitOfWork unitOfWork)
        {
            _productTypeRepository = productTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SaveProductTypeResponse> DeleteAsync(int id)
        {
            var existingProductType = await _productTypeRepository.FindByIdAsync(id);


            if (existingProductType == null)
                return new SaveProductTypeResponse("ProductType not found.");


            try
            {
                _productTypeRepository.Remove(existingProductType);
                await _unitOfWork.CompleteAsync();


                return new SaveProductTypeResponse(existingProductType);
            }
            catch (Exception ex)
            {

                return new SaveProductTypeResponse($"An error occurred when deleting the ProductType: {ex.Message}");
            }
        }

        public async Task<ProductType> FindByIdAsync(int id)
        {
            return await _productTypeRepository.FindByIdAsync(id);
        }

        public async Task<IEnumerable<ProductType>> ListAsync()
        {
            return await _productTypeRepository.ListAsync();
        }

        public async Task<SaveProductTypeResponse> SaveAsync(ProductType productType)
        {
            try
            {
                await _productTypeRepository.AddAsync(productType);
                await _unitOfWork.CompleteAsync();

                return new SaveProductTypeResponse(productType);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SaveProductTypeResponse($"An error occurred when saving the ProductType: {ex.Message}");
            }
        }

        public async Task<SaveProductTypeResponse> UpdateAsync(int id, ProductType productType)
        {
            var exixtingProductType = await _productTypeRepository.FindByIdAsync(id);
            if (exixtingProductType == null)
                return new SaveProductTypeResponse("ProductType not found.");

            exixtingProductType.Name = productType.Name;
            exixtingProductType.Description = productType.Description;

            try
            {
                _productTypeRepository.Update(exixtingProductType);
                await _unitOfWork.CompleteAsync();


                return new SaveProductTypeResponse(exixtingProductType);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SaveProductTypeResponse($"An error occurred when updating the productType: {ex.Message}");
            }
        }
    }
}
