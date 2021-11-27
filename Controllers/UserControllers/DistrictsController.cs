using Microsoft.AspNetCore.Mvc;
using ofarz_rest_api.Domain.IService.IUserServices;
using ofarz_rest_api.Domain.Models.User;
using ofarz_rest_api.Extensions;
using ofarz_rest_api.Resources.UserResources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ofarz_rest_api.Controllers
{
    [Route("/api/[controller]")]
    public class DistrictsController : Controller
    {
        private readonly IDistrictService _districtService;

        public DistrictsController(IDistrictService districtService)
        {
            _districtService = districtService;
        }

        [HttpGet]
        public async Task<IEnumerable<District>> GetAllAsync()
        {
            var districts = await _districtService.ListAsync();
            return districts;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync(int id)
        {
            var district = await _districtService.FindByIdAsync(id);
            return Ok(district);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] District district)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _districtService.SaveAsync(district);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] DistrictResource district)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _districtService.UpdateAsync(id, district);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(district);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _districtService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
