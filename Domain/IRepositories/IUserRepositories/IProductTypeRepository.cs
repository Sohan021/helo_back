using ofarz_rest_api.Domain.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ofarz_rest_api.Domain.IRepositories.IUserRepositories
{
    public interface IProductTypeRepository
    {
        Task<IEnumerable<ProductType>> ListAsync();
        Task<ProductType> FindByIdAsync(int id);
        ProductType GetProductType(int id);
        Task AddAsync(ProductType productType);
        void Update(ProductType productType);
        void Remove(ProductType productType);
    }
}
