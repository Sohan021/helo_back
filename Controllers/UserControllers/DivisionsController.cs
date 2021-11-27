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
    public class DivisionsController : Controller
    {
        private readonly IDivisionService _divisionService;

        public DivisionsController(IDivisionService divisionService)
        {
            _divisionService = divisionService;
        }

        [HttpGet]
        public async Task<IEnumerable<Division>> GetAllAsync()
        {
            var divisions = await _divisionService.ListAsync();
            return divisions;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync(int id)
        {
            var division = await _divisionService.FindByIdAsync(id);
            return Ok(division);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Division division)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _divisionService.SaveAsync(division);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] DivisionResource division)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _divisionService.UpdateAsync(id, division);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(division);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _divisionService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
