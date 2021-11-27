
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
    public class UnionsController : Controller
    {
        private readonly IUnionService _unionService;
        //private readonly IMapper _mapper;

        public UnionsController(IUnionService unionService)
        {
            _unionService = unionService;
            //_mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<UnionOrWard>> GetAllAsync()
        {
            var unionorwards = await _unionService.ListAsync();
            return unionorwards;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync(int id)
        {
            var union = await _unionService.FindByIdAsync(id);
            return Ok(union);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] UnionOrWard unionorward)
        {


            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());


            var result = await _unionService.SaveAsync(unionorward);

            if (!result.Success)
                return BadRequest(result.Message);


            return Ok(result);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] UnionOrWardResource union)
        {


            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());


            var result = await _unionService.UpdateAsync(id, union);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(union);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _unionService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
