using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ofarz_rest_api.Domain.Models.Account;
using ofarz_rest_api.Domain.Models.Fund;
using ofarz_rest_api.Persistence.Context;
using ofarz_rest_api.Resources.AuthResources.RegistrationResources;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ofarz_rest_api.Controllers.AccountControllers
{
    [Route("api/[controller]/[action]")]
    public class OfarzController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _configuration;

        private readonly AppDbContext _context;

        public OfarzController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            IHostingEnvironment env,
            IConfiguration configuration,
            AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _env = env;
            _configuration = configuration;
            _context = context;
        }


        [HttpPost]
        public async Task<object> AddOfarz([FromBody] OfarzRegistrationResource resource)
        {

            var ofarzRole = _roleManager.Roles.Where(r => r.Name == "Ofarz").FirstOrDefault();

            var ofarz = new ApplicationUser
            {
                FirstName = resource.Name,
                UserName = resource.MobileNumber,
                NormalizedUserName = resource.MobileNumber,
                Email = resource.Email,
                PhoneNumber = resource.MobileNumber,
                ApplicationRole = ofarzRole
            };


            var result = await _userManager.CreateAsync(ofarz, resource.Password);

            var ofarzFund = new OfarzFund
            {
                OfarzId = ofarz.Id,
                MainAccount = 0.0,
                GetMoneyByAgent = 0.0,
                GetMoneyByAppSharer = 0.0,
                GetMoneyByKarrot = 0.0,
                GetMoneyByCeo = 0.0,
                GetMoneyByAgentShopping = 0.0,
                MobileNumber = ofarz.PhoneNumber
            };

            await _context.AddAsync(ofarzFund);
            await _context.SaveChangesAsync();

            if (result.Succeeded)
            {
                return Ok();
            }

            throw new ApplicationException("UNKNOWN_ERROR");
        }

    }
}
