using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ofarz_rest_api.Domain.IService.IUserServices;
using ofarz_rest_api.Domain.Models.User;
using ofarz_rest_api.Extensions;
using ofarz_rest_api.Persistence.Context;
using ofarz_rest_api.Resources.UserResources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ofarz_rest_api.Controllers.UserController
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductService _productService;
        private IHostingEnvironment _env;
        private AppDbContext _context;

        public ProductsController(AppDbContext context,
                                  IProductService productService,

                                  IHostingEnvironment env

                                  )
        {

            _productService = productService;
            _env = env;
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = await _productService.ListAsync();
            return products;
        }

        [HttpGet("{agentCode}")]
        public async Task<IActionResult> GetProductsForAgentAndCustomer([FromRoute] int agentCode)
        {
            var products = await _context
                            .AgentOrderDetails
                            .Where(_ => _.AgentOrder.User.AgentCode == agentCode)
                            .Select(p => p.Product)
                            .Include(_ => _.ProductType)
                            .Include(_ => _.Category)
                            .Include(_ => _.SubCategory)
                            .ToListAsync();

            return Ok(products);

        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync(int id)
        {
            var product = await _productService.FindByIdAsync(id);
            return Ok(product);

        }

        [HttpGet("{productTypeId}/{categoryId}/{subCategoryId}")]
        public async Task<IActionResult> GetProductsByProductTypeAndCategory([FromRoute]int productTypeId, [FromRoute]int categoryId, [FromRoute]int subCategoryId)
        {
            var products = await _context.Products
                .Where(_ => _.CategoryId == categoryId && _.SubCategoryId == subCategoryId && _.ProductTypeId == productTypeId)
                .Include(_ => _.Category)
                .Include(_ => _.ProductType)
                .Include(_ => _.SubCategory)
                .ToListAsync();
            return Ok(products);

        }


        [HttpGet("{agentCode}/{productTypeId}/{categoryId}/{subCategoryId}")]
        public async Task<IActionResult> GetProductsForCustomer([FromRoute] int agentCode, [FromRoute]int productTypeId, [FromRoute]int categoryId, [FromRoute]int subCategoryId)
        {

            var products = await _context
                            .AgentOrderDetails
                            .Where(_ => _.AgentOrder.User.AgentCode == agentCode
                                     && _.Product.CategoryId == categoryId
                                     && _.Product.SubCategoryId == subCategoryId
                                     && _.Product.ProductTypeId == productTypeId)
                            .Select(p => p.Product)
                            .Include(_ => _.ProductType)
                            .Include(_ => _.Category)
                            .Include(_ => _.SubCategory)
                            .ToListAsync();

            return Ok(products);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAgentProduct([FromRoute] int id)
        {

            var product = await _context.AgentOrderDetails.Where(_ => _.ProductId == id).FirstOrDefaultAsync();

            _context.Remove(product);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost, DisableRequestSizeLimit]
        //[ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> SavePhoto()
        {
            var files = Request.Form.Files as List<IFormFile>;
            string imageUrl = ImageUrl(files[0]);
            return Ok(await Task.FromResult(imageUrl));
        }


        [HttpPost]
        //[ProducesResponseType(typeof(ProductResourceViewModel), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostAsync(ProductResource resource)
        {
            var cat = _context.Categories.Where(_ => _.Id == resource.CategoryId).FirstOrDefault();
            var pType = _context.ProductTypes.Where(_ => _.Id == resource.ProductTypeId).FirstOrDefault();
            var subCat = _context.SubCategories.Where(_ => _.Id == resource.SubCategoryId).FirstOrDefault();

            var webRoot = _env.WebRootPath;
            var PathWithFolderName = Path.Combine(webRoot, "Image");

            var product = new Product
            {
                Name = resource.Name,
                Price = resource.Price,
                CountInStock = resource.CountInStock,
                ProductCode = resource.ProductCode,
                //CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                ImageUrl = resource.ImageUrl,
                Description = resource.Description,
                Category = cat,
                SubCategory = subCat,
                ProductType = pType

            };

            var result = await _productService.SaveAsync(product);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return Ok(result);




        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, ProductResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var webRoot = _env.WebRootPath;
            var PathWithFolderName = System.IO.Path.Combine(webRoot, "Image");
            var item = resource.File;

            var imageUrl = ImageUrl(item);

            var product = new Product
            {
                Name = resource.Name,
                Price = resource.Price,
                CountInStock = resource.CountInStock,
                ProductCode = resource.ProductCode,
                //CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                ImageUrl = imageUrl,
                Description = resource.Description,
                CategoryId = resource.CategoryId,
                ProductTypeId = resource.ProductTypeId
            };


            var result = await _productService.UpdateAsync(id, product);


            if (!result.Success)
                return BadRequest(result.Message);



            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _productService.DeleteAsync(id);


            if (!result.Success)
                return BadRequest(result.Message);


            //var productResource = _mapper.Map<Product, ProductResourceViewModel>(result.Resource);
            return Ok(result);
        }


        public string ImageUrl(IFormFile file)
        {


            if (file == null || file.Length == 0) return null;
            string extension = Path.GetExtension(file.FileName);

            string path_Root = _env.WebRootPath;

            string path_to_Images = path_Root + "\\Image\\" + file.FileName;

            using (var stream = new FileStream(path_to_Images, FileMode.Create))
            {

                file.CopyTo(stream);
                string revUrl = Reverse.reverse(path_to_Images);
                int count = 0;
                int flag = 0;

                for (int i = 0; i < revUrl.Length; i++)
                {
                    if (revUrl[i] == '\\')
                    {
                        count++;

                    }
                    if (count == 2)
                    {
                        flag = i;
                        break;
                    }
                }

                string sub = revUrl.Substring(0, flag + 1);
                string finalString = Reverse.reverse(sub);

                string f = finalString.Replace("\\", "/");
                return f;

            }


        }
    }

    public static class Reverse
    {
        public static string reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

    }
}
