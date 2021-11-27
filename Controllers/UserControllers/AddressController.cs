using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ofarz_rest_api.Persistence.Context;
using System.Linq;
using System.Threading.Tasks;

namespace ofarz_rest_api.Controllers.UserControllers
{
    [Route("/api/[controller]/[action]")]
    public class AddressController : Controller
    {
        private readonly AppDbContext _context;

        public AddressController(AppDbContext context)
        {
            _context = context;
        }



        [HttpGet]
        public async Task<IActionResult> GetAreaCode()
        {
            var area = await _context.ApplicationUsers
                                .Where(_ => _.ApplicationRole.Name == "Agent")
                                .Include(_ => _.Market)
                                .ThenInclude(_ => _.UnionOrWard)
                                .ThenInclude(_ => _.Upozila)
                                .ThenInclude(_ => _.District)
                                .ThenInclude(_ => _.Division)
                                .ThenInclude(_ => _.Country)
                                .ToListAsync();
            return Ok(area);
        }



        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            var countries = await _context.Countries.ToListAsync();
            return Ok(countries);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDivisions(int id)
        {
            var divisions = await _context.Divisions.Where(_ => _.CountryId == id).ToListAsync();
            return Ok(divisions);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDistricts(int id)
        {
            var districts = await _context.Districts.Where(_ => _.DivisionId == id).ToListAsync();
            return Ok(districts);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUpozilas(int id)
        {
            var upozilas = await _context.Upozillas.Where(_ => _.DistrictId == id).ToListAsync();
            return Ok(upozilas);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUnions(int id)
        {
            var unionOrWards = await _context.UnionOrWards.Where(_ => _.UpozilaId == id).ToListAsync();
            return Ok(unionOrWards);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMarkets(int id)
        {
            var markets = await _context.Markets.Where(_ => _.UnionOrWardId == id).ToListAsync();
            return Ok(markets);
        }
    }
}
