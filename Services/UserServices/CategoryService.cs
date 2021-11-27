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
    public class CategoryService : ICategoryService
    {

        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;


        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Category>> ListAsync()
        {
            return await _categoryRepository.ListAsync();
        }

        public async Task<SaveCategoryResponse> SaveAsync(Category category)
        {
            try
            {
                await _categoryRepository.AddAsync(category);
                await _unitOfWork.CompleteAsync();

                return new SaveCategoryResponse(category);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SaveCategoryResponse($"An error occurred when saving the category: {ex.Message}");
            }
        }

        public async Task<SaveCategoryResponse> UpdateAsync(int id, CategoryResource resource)
        {
            var existingCategory = await _categoryRepository.FindByIdAsync(id);

            if (existingCategory == null)
                return new SaveCategoryResponse("Category not found.");

            existingCategory.Name = resource.Name;
            existingCategory.Description = resource.Description;

            try
            {
                _categoryRepository.Update(existingCategory);
                await _unitOfWork.CompleteAsync();


                return new SaveCategoryResponse(existingCategory);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SaveCategoryResponse($"An error occurred when updating the category: {ex.Message}");
            }

        }

        public async Task<SaveCategoryResponse> DeleteAsync(int id)
        {
            var existingCategory = await _categoryRepository.FindByIdAsync(id);


            if (existingCategory == null)
                return new SaveCategoryResponse("Category not found.");


            try
            {
                _categoryRepository.Remove(existingCategory);
                await _unitOfWork.CompleteAsync();


                return new SaveCategoryResponse(existingCategory);
            }
            catch (Exception ex)
            {

                return new SaveCategoryResponse($"An error occurred when deleting the category: {ex.Message}");
            }
        }

        public async Task<Category> FindByIdAsync(int id)
        {
            return await _categoryRepository.FindByIdAsync(id);
        }

        public Category GetCategory(int id)
        {
            return _categoryRepository.GetCategory(id);
        }
    }
}
