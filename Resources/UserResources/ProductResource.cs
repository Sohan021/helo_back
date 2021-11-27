using Microsoft.AspNetCore.Http;
using ofarz_rest_api.Domain.Models.User;

namespace ofarz_rest_api.Resources.UserResources
{
    //[ModelBinder(typeof(JsonWithFilesFormDataModelBinder), Name = "json")]
    public class ProductResource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public bool IsAvailabe { get; set; }

        public int CountInStock { get; set; }

        public string ProductCode { get; set; }

        public IFormFile File { get; set; }

        public string ImageUrl { get; set; }

        public int? CategoryId { get; set; }

        public Category Category { get; set; }

        public int? SubCategoryId { get; set; }

        public SubCategory SubCategory { get; set; }

        public int? ProductTypeId { get; set; }

        public ProductType ProductType { get; set; }
    }
}
