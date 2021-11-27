using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ofarz_rest_api.Domain.Models.Account;
using ofarz_rest_api.Domain.Models.Fund;
using ofarz_rest_api.Persistence.Context;
using ofarz_rest_api.Resources.AuthResources.RegistrationResources;
using ofarz_rest_api.Resources.FundResources.Withdraw;
using ofarz_rest_api.Resources.UserResources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ofarz_rest_api.Controllers.AccountControllers
{
    [Route("api/[controller]/[action]")]
    public class KarrotController : Controller
    {

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _configuration;

        private readonly AppDbContext _context;

        public KarrotController(UserManager<ApplicationUser> userManager,
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

        [HttpPost, DisableRequestSizeLimit]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> SavePhoto()
        {
            var files = Request.Form.Files as List<IFormFile>;
            string imageUrl = ImageUrl(files[0]);
            return Ok(await Task.FromResult(imageUrl));
        }

        [HttpPost]
        public async Task<object> AddKarrot([FromBody] KarrotRegistrationResource resource)
        {

            var webRoot = _env.WebRootPath;

            var PathWithFolderName = Path.Combine(webRoot, "Image");

            var agentRole = _roleManager.Roles.Where(r => r.Name == "Karrot").FirstOrDefault();

            var karrot = new ApplicationUser
            {
                FirstName = resource.Name,
                UserName = resource.MobileNumber,
                NormalizedUserName = resource.MobileNumber,
                Email = resource.Email,
                PhoneNumber = resource.MobileNumber,
                ProfilePhoto = resource.KarrotLogo,
                ApplicationRole = agentRole
            };


            var result = await _userManager.CreateAsync(karrot, resource.Password);

            var karrotFund = new KarrotFund
            {
                KarrotId = karrot.Id,
                MainAccount = 0.0,
                TotalIncome = 0.0,
                KarrotCode = 21873465
            };

            await _context.AddAsync(karrotFund);
            await _context.SaveChangesAsync();

            if (result.Succeeded)
            {
                return Ok();
            }

            throw new ApplicationException("UNKNOWN_ERROR");
        }


        [HttpPost]
        public async Task<object> WithdrawMoneyKarrotToOfarz([FromBody] WithdrawMoneyKarrotToOfarzResource withdraw)
        {
            var currentUserId = withdraw.KarrotId;

            var currentUserDetails = _userManager.Users.Where(_ => _.Id == currentUserId).FirstOrDefault();

            var role = _context.Roles.Where(_ => _.Id == currentUserDetails.ApplicationRoleId).FirstOrDefault();

            var KarrotFundExist = _context.KarrotFunds.Where(_ => _.KarrotId == currentUserId).FirstOrDefault();


            if (KarrotFundExist.MainAccount < withdraw.Amount)
            {
                return Ok("You have Not enough Money For CashOut");
            }

            if (KarrotFundExist.MainAccount >= withdraw.Amount)
            {
                var withdrawMoney = new WithdrawMoney
                {
                    Amount = withdraw.Amount,
                    UserId = currentUserId,
                    PaymentTime = DateTime.Now,
                    OfarzPhoneNumber = withdraw.OfarzPhoneNumber,
                    UserName = currentUserDetails.FirstName,
                    UserPhoneNumber = currentUserDetails.PhoneNumber,
                };

                double amount = withdraw.Amount;

                var findOfarz = _userManager.Users
                                .Where(_ => _.PhoneNumber == withdraw.OfarzPhoneNumber)
                                .FirstOrDefault();

                if (findOfarz != null)
                {
                    var ofarzFundExist = _context.OfarzFunds.Where(_ => _.OfarzId == findOfarz.Id).FirstOrDefault();


                    if (ofarzFundExist != null)
                    {

                        ofarzFundExist.GetMoneyByKarrot = ofarzFundExist.GetMoneyByKarrot + amount;

                        ofarzFundExist.MainAccount = ofarzFundExist.MainAccount + amount;

                        _context.Update(ofarzFundExist);
                        _context.SaveChanges();

                    }
                }

                if (findOfarz == null)
                {
                    return (currentUserDetails, role, KarrotFundExist);
                }

                KarrotFundExist.MainAccount = KarrotFundExist.MainAccount - amount;
                _context.Update(KarrotFundExist);
                _context.SaveChanges();

                await _context.AddAsync(withdrawMoney);
                await _context.SaveChangesAsync();

                return (currentUserDetails, role, KarrotFundExist);
            }


            return Ok();
        }


        public async Task<object> WithdrawMoneyKarrotToAgent([FromBody] WithdrawMoneyKarrotToAgentResource withdraw)
        {
            var currentUserId = withdraw.KarrotId;

            var currentUserDetails = _userManager.Users.Where(_ => _.Id == currentUserId).FirstOrDefault();

            var role = _context.Roles.Where(_ => _.Id == currentUserDetails.ApplicationRoleId).FirstOrDefault();

            var KarrotFundExist = _context.KarrotFunds.Where(_ => _.KarrotId == currentUserId).FirstOrDefault();


            if (KarrotFundExist.MainAccount < withdraw.Amount)
            {
                return Ok("You have Not enough Money For CashOut");
            }

            if (KarrotFundExist.MainAccount >= withdraw.Amount)
            {
                var withdrawMoney = new WithdrawMoney
                {
                    Amount = withdraw.Amount,
                    UserId = currentUserId,
                    PaymentTime = DateTime.Now,
                    AgentPhnNumber = withdraw.AgentPhoneNumber,
                    UserName = currentUserDetails.FirstName,
                    UserPhoneNumber = currentUserDetails.PhoneNumber,
                };

                double amount = withdraw.Amount;

                var findAgent = _userManager.Users
                                .Where(_ => _.PhoneNumber == withdraw.AgentPhoneNumber)
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

                if (findAgent == null)
                {
                    return (currentUserDetails, role, KarrotFundExist);
                }

                KarrotFundExist.MainAccount = KarrotFundExist.MainAccount - amount;
                _context.Update(KarrotFundExist);
                _context.SaveChanges();

                await _context.AddAsync(withdrawMoney);
                await _context.SaveChangesAsync();

                return (currentUserDetails, role, KarrotFundExist);
            }


            return Ok();
        }


        public string ImageUrl(IFormFile file)
        {


            if (file == null || file.Length == 0) return null;
            string extension = Path.GetExtension(file.FileName);

            string path_Root = _env.WebRootPath;

            string path_to_Images = path_Root + "\\Image\\" + file.FileName;

            using (var stream = new FileStream(path_to_Images, FileMode.Create))
            {

                file.CopyTo(stream);
                string revUrl = ReverseKarrot.reverse(path_to_Images);
                int count = 0;
                int flag = 0;

                for (int i = 0; i < revUrl.Length; i++)
                {
                    if (revUrl[i] == '\\')
                    {
                        count++;

                    }
                    if (count == 2)
                    {
                        flag = i;
                        break;
                    }
                }

                string sub = revUrl.Substring(0, flag + 1);
                string finalString = ReverseKarrot.reverse(sub);

                string f = finalString.Replace("\\", "/");
                return f;

            }
        }
    }

    public static class ReverseKarrot
    {
        public static string reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

    }
}
