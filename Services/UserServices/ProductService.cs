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
    public class ProductService : IProductService
    {


        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;




        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;

        }

        public async Task<IEnumerable<Product>> ListAsync()
        {
            return await _productRepository.ListAsync();
        }


        public async Task<SaveProductResponse> SaveAsync(Product product)
        {
            try
            {
                await _productRepository.AddAsync(product);
                await _unitOfWork.CompleteAsync();

                return new SaveProductResponse(product);
            }
            catch (Exception ex)
            {
                return new SaveProductResponse($"An error occurred when saving the Product: {ex.Message}");
            }
        }

        public async Task<SaveProductResponse> UpdateAsync(int id, Product product)
        {
            var existingProduct = await _productRepository.FindByIdAsync(id);

            if (existingProduct == null)
                return new SaveProductResponse("Product not found.");


            //existingProduct.Name = product.Name;
            //existingProduct.Description = product.Description;
            //existingProduct.Price = product.Price;
            //existingProduct.ImageUrl = product.ImageUrl;
            //existingProduct.Category = product.Category;
            //existingProduct.SubCategory = product.SubCategory;

            try
            {
                _productRepository.Update(existingProduct);
                await _unitOfWork.CompleteAsync();


                return new SaveProductResponse(existingProduct);
            }
            catch (Exception ex)
            {
                return new SaveProductResponse($"An error occurred when updating the Product: {ex.Message}");
            }
        }

        public async Task<SaveProductResponse> DeleteAsync(int id)
        {
            var existingProduct = await _productRepository.FindByIdAsync(id);


            if (existingProduct == null)
                return new SaveProductResponse("Product not found.");


            try
            {
                _productRepository.Remove(existingProduct);
                await _unitOfWork.CompleteAsync();


                return new SaveProductResponse(existingProduct);
            }
            catch (Exception ex)
            {

                return new SaveProductResponse($"An error occurred when deleting the Product: {ex.Message}");
            }
        }

        public async Task<Product> FindByIdAsync(int id)
        {
            return await _productRepository.FindByIdAsync(id);
        }
    }
}
