using Microsoft.AspNetCore.Mvc;
using ofarz_rest_api.Domain.IService.IUserServices;
using ofarz_rest_api.Domain.Models.User;
using ofarz_rest_api.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ofarz_rest_api.Controllers.UserController
{
    [Route("/api/[controller]")]
    public class ProductTypesController : Controller
    {
        private readonly IProductTypeService _productTypeService;

        public ProductTypesController(IProductTypeService productTypeService)
        {
            _productTypeService = productTypeService;
        }


        [HttpGet]
        public async Task<IEnumerable<ProductType>> GetAllAsync()
        {
            var productTypes = await _productTypeService.ListAsync();
            return productTypes;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync(int id)
        {
            var productType = await _productTypeService.FindByIdAsync(id);
            return Ok(productType);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProductType), 201)]
        //[ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostAsync([FromBody] ProductType productType)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _productTypeService.SaveAsync(productType);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);



        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] ProductType productType)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _productTypeService.UpdateAsync(id, productType);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(productType);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _productTypeService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
