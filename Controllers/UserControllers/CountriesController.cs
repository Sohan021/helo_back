using Microsoft.AspNetCore.Mvc;
using ofarz_rest_api.Domain.IService.IUserServices;
using ofarz_rest_api.Domain.Models.User;
using ofarz_rest_api.Extensions;
using ofarz_rest_api.Persistence.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ofarz_rest_api.Controllers
{

    [Route("/api/[controller]")]
    public class CountriesController : Controller
    {
        private readonly ICountryService _countryService;
        private readonly AppDbContext _context;

        public CountriesController(ICountryService countryService,
            AppDbContext context)
        {
            _countryService = countryService;
            _context = context;
        }


        [HttpGet]
        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            var countries = await _countryService.ListAsync();

            return countries;

        }

        //[HttpGet]
        //public async Task<IEnumerable<Country>> GetCountryDivisions()
        //{
        //    var divisions =  _context.Countries.Select(_ => _.Divisions).ToList();

        //    return Ok(divisions);

        //}


        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync(int id)
        {
            var country = await _countryService.FindByIdAsync(id);

            return Ok(country);
        }

        [HttpPost]
        //[ProducesResponseType(typeof(Country), 201)]
        //[ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostAsync([FromBody]Country country)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _countryService.SaveAsync(country);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
            //return RedirectToAction("Index", "CountryClient");


        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Country country)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _countryService.UpdateAsync(id, country);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(country);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _countryService.DeleteAsync(id);

            if (!result.Success)

                return BadRequest(result.Message);

            return Ok(result);
        }

    }
}
