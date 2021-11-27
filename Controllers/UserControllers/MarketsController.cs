using Microsoft.AspNetCore.Mvc;
using ofarz_rest_api.Domain.IService.IUserServices;
using ofarz_rest_api.Domain.Models.User;
using ofarz_rest_api.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ofarz_rest_api.Controllers
{

    [Route("/api/[controller]")]
    public class MarketsController : Controller
    {
        private readonly IMarketService _marketService;

        public MarketsController(IMarketService marketService)
        {
            _marketService = marketService;
        }


        [HttpGet]
        public async Task<IEnumerable<Market>> GetAllAsync()
        {
            var Markets = await _marketService.ListAsync();

            return Markets;

            //return View("Index", "Address");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync(int id)
        {
            var market = await _marketService.FindByIdAsync(id);

            return Ok(market);
        }

        [HttpPost]
        //[ProducesResponseType(typeof(Country), 201)]
        //[ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostAsync([FromBody]Market market)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _marketService.SaveAsync(market);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
            //return RedirectToAction("Index", "CountryClient");


        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Market market)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _marketService.UpdateAsync(id, market);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(market);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _marketService.DeleteAsync(id);

            if (!result.Success)

                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
