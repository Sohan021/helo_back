using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class ShoperController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public ShoperController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            IHostingEnvironment env,
            IConfiguration configuration,
            AppDbContext context
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _env = env;
            _configuration = configuration;
            _context = context;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllAppShoper()
        {
            var shopers = await _userManager.Users
                .Where(_ => _.ApplicationRole.Name == "Shoper").ToListAsync();

            return Ok(shopers);
        }



        [HttpPost]
        public async Task<object> ShoperRegistration([FromBody] ShoperRegistrationResource shoperRegistrationResource)
        {

            var customerRole = _roleManager.Roles.Where(r => r.Name == "Shoper").FirstOrDefault();

            var shoper = new ApplicationUser
            {
                UserName = shoperRegistrationResource.MobileNumber,
                PhoneNumber = shoperRegistrationResource.MobileNumber,
                FirstName = shoperRegistrationResource.FirstName,
                LastName = shoperRegistrationResource.LastName,
                ApplicationRole = customerRole
            };

            var result = await _userManager.CreateAsync(shoper, shoperRegistrationResource.Password);

            if (result.Succeeded)
            {


                var shoperFund = new ShoperFund
                {
                    ShoperId = shoper.Id,
                    BackShoppingAccount = 0.0,
                    ShoperPhoneNumber = shoper.PhoneNumber,
                    ShoperName = shoper.FirstName + " " + shoper.LastName

                };

                _context.Add(shoperFund);
                _context.SaveChanges();



                return Ok();
            }

            throw new ApplicationException("UNKNOWN_ERROR");
        }



        [HttpPost]
        public async Task<IActionResult> MakePaymentViaDirectCashOffer([FromBody] Payment payment)
        {
            var currentUser = payment.PayerId;

            var currentUserDetails = _userManager.Users.Where(_ => _.Id == currentUser).FirstOrDefault();

            var productType = _context.ProductTypes.Where(_ => _.Name == "Offer").FirstOrDefault();

            var paymentType = _context.PaymentTypes.Where(_ => _.PaymentTypeName == "TableCashOffer").FirstOrDefault();

            var pay = new Payment
            {
                Id = payment.Id,
                Amount = payment.Amount,
                PayerId = currentUser,
                PaymentTime = DateTime.UtcNow,
                AgentPhnNumber = payment.AgentPhnNumber,
                PayerName = currentUserDetails.FirstName,
                PayerPhoneNumber = currentUserDetails.PhoneNumber,
                ProductType = productType,
                PaymentType = paymentType
            };



            var amount = payment.Amount;

            var findAgent = _userManager.Users
                               .Where(_ => _.PhoneNumber == payment.AgentPhnNumber)
                               .FirstOrDefault();

            if (findAgent != null)
            {
                var agentFundExist = _context.AgentFunds.Where(_ => _.AgentId == findAgent.Id).FirstOrDefault();


                if (agentFundExist != null)
                {
                    agentFundExist.ShoperTransection = agentFundExist.ShoperTransection + ((amount * 3) / 100);

                    agentFundExist.SellViaDirectCash = agentFundExist.SellViaDirectCash + amount;

                    agentFundExist.TotalTransection = agentFundExist.TotalTransection + amount;

                    _context.Update(agentFundExist);
                    _context.SaveChanges();

                }
            }



            var userAccount = _context.ShoperFunds.Where(_ => _.ShoperId == currentUser).FirstOrDefault();


            if (userAccount != null)
            {
                userAccount.BackShoppingAccount = (userAccount.BackShoppingAccount + ((50.00 * amount) / 100.0));
                _context.Update(userAccount);
                _context.SaveChanges();
            }

            await _context.AddAsync(pay);
            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> MakePaymentViaBackShoppingOffer([FromBody] Payment payment)
        {
            var currentUser = payment.PayerId;

            var currentUserDetails = _userManager.Users.Where(_ => _.Id == currentUser).FirstOrDefault();

            var productType = _context.ProductTypes.Where(_ => _.Name == "Offer").FirstOrDefault();

            var fundExist = _context.ShoperFunds.Where(_ => _.ShoperId == currentUser).FirstOrDefault();

            var paymentType = _context.PaymentTypes.Where(_ => _.PaymentTypeName == "BackShoppingOffer").FirstOrDefault();

            if (fundExist.BackShoppingAccount < payment.Amount)
            {
                return Ok("You have Not enough Money at BackShopping Account");
            }

            if (fundExist.BackShoppingAccount >= payment.Amount)
            {
                var pay = new Payment
                {
                    Amount = payment.Amount,
                    PayerId = currentUser,
                    PaymentTime = DateTime.UtcNow,
                    AgentPhnNumber = payment.AgentPhnNumber,
                    PayerName = currentUserDetails.FirstName,
                    PayerPhoneNumber = currentUserDetails.PhoneNumber,
                    ProductType = productType,
                    PaymentType = paymentType
                };

                double amount = payment.Amount;

                var findAgent = _userManager.Users
                               .Where(_ => _.PhoneNumber == payment.AgentPhnNumber)
                               .FirstOrDefault();

                if (findAgent != null)
                {
                    var agentFundExist = _context.AgentFunds.Where(_ => _.AgentId == findAgent.Id).FirstOrDefault();

                    if (agentFundExist != null)
                    {

                        agentFundExist.MainAccount = agentFundExist.MainAccount + amount;

                        agentFundExist.TotalTransection = agentFundExist.TotalTransection + amount;

                        _context.Update(agentFundExist);
                        _context.SaveChanges();

                    }
                }

                fundExist.BackShoppingAccount = fundExist.BackShoppingAccount - amount;
                _context.Update(fundExist);
                await _context.SaveChangesAsync();

                await _context.AddAsync(pay);
                await _context.SaveChangesAsync();

                return Ok("Your Payment is Completed via BackShopping -- Offer");
            }
            return Ok();
        }



    }


}
