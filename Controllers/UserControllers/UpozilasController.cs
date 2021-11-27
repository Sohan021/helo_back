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
    public class UpozilasController : Controller
    {
        private readonly IUpozillaService _upozillaService;

        public UpozilasController(IUpozillaService upozillaService)
        {
            _upozillaService = upozillaService;
        }

        [HttpGet]
        public async Task<IEnumerable<Upozila>> GetAllAsync()
        {
            var upozilla = await _upozillaService.ListAsync();
            return upozilla;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync(int id)
        {
            var upozilla = await _upozillaService.FindByIdAsync(id);
            return Ok(upozilla);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Upozila upozilla)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _upozillaService.SaveAsync(upozilla);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] UpozilaResource upozilla)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _upozillaService.UpdateAsync(id, upozilla);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(upozilla);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _upozillaService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
