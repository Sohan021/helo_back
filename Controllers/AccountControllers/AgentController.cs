using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ofarz_rest_api.Domain.Models.Account;
using ofarz_rest_api.Domain.Models.Fund;
using ofarz_rest_api.Persistence.Context;
using ofarz_rest_api.Resources.AuthResources.LoginResources;
using ofarz_rest_api.Resources.AuthResources.ProfileResources;
using ofarz_rest_api.Resources.AuthResources.RegistrationResources;
using ofarz_rest_api.Resources.FundResources.Withdraw;
using ofarz_rest_api.Resources.UserResources;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ofarz_rest_api.Controllers.AccountControllers
{
    [Route("api/[controller]/[action]")]
    public class AgentController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _configuration;

        private readonly AppDbContext _context;

        public AgentController(
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
        public async Task<IActionResult> GetAllAgents()
        {
            var agents = await _userManager.Users
                .Where(_ => _.ApplicationRole.Name == "Agent").AsNoTracking().ToListAsync();

            return Ok(agents);
        }


        [HttpGet("{agentCode}")]
        public async Task<IActionResult> FindAgent([FromRoute] int agentCode)
        {
            var agent = await _userManager.Users
                                .Where(_ => _.AgentCode == agentCode)
                                .AsNoTracking()
                                .FirstOrDefaultAsync();

            return Ok(agent);
        }



        [HttpPost]
        public async Task<object> AgentSignin([FromBody] AgentLoginResource model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.MobileNumber, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.UserName == model.MobileNumber);
                var token = GenerateJwtToken(model.MobileNumber, appUser);
                return (appUser, token);
            }

            throw new ApplicationException();
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
        public async Task<object> AddAgent([FromBody] AgentRegistrationResource agentRegistrationResource)
        {
            var webRoot = _env.WebRootPath;

            var PathWithFolderName = Path.Combine(webRoot, "Image");

            var agentRole = _roleManager.Roles.Where(r => r.Name == "Agent").FirstOrDefault();

            var agentCode = _userManager.Users.Where(_ => _.ApplicationRole.Name == "Agent").Count();


            var country = _context.Countries.Where(_ => _.Id == agentRegistrationResource.CountryId).FirstOrDefault();
            var division = _context.Divisions.Where(_ => _.Id == agentRegistrationResource.DivisionId).FirstOrDefault();
            var district = _context.Districts.Where(_ => _.Id == agentRegistrationResource.DistrictId).FirstOrDefault();
            var upozila = _context.Upozillas.Where(_ => _.Id == agentRegistrationResource.UpozilaId).FirstOrDefault();
            var union = _context.UnionOrWards.Where(_ => _.Id == agentRegistrationResource.UnionOrWardId).FirstOrDefault();
            var market = _context.UnionOrWards.Where(_ => _.Id == agentRegistrationResource.MarketId).FirstOrDefault();


            var agent = new ApplicationUser
            {
                UserName = agentRegistrationResource.MobileNumber,
                NormalizedUserName = agentRegistrationResource.MobileNumber,
                FirstName = agentRegistrationResource.FirstName,
                LastName = agentRegistrationResource.LastName,
                Email = agentRegistrationResource.Email,
                PhoneNumber = agentRegistrationResource.MobileNumber,
                NID_Number = agentRegistrationResource.NID_Number,
                ProfilePhoto = agentRegistrationResource.ProfilePhoto,
                PostalCode = agentRegistrationResource.PostalCode,
                AgentShopName = agentRegistrationResource.ShopName,
                CountryId = agentRegistrationResource.CountryId,
                DivisionId = agentRegistrationResource.DivisionId,
                DistrictId = agentRegistrationResource.DistrictId,
                UpozilaId = agentRegistrationResource.UpozilaId,
                UnionOrWardId = agentRegistrationResource.UnionOrWardId,
                MarketId = agentRegistrationResource.MarketId,

                CountryName = country.Name,
                DivisionName = division.Name,
                DistrictName = district.Name,
                UpozilaName = upozila.Name,
                UnionOrWardName = union.Name,

                AgentCode = agentCode + 1,
                ApplicationRole = agentRole
            };




            var result = await _userManager.CreateAsync(agent, agentRegistrationResource.Password);

            var agentFund = new AgentFund
            {
                AgentId = agent.Id,
                MainAccount = 0.0,
                SellViaDirectCash = 0.0,
                TotalTransection = 0.0,
                AgentName = agent.FirstName,
                AgentPhoneNumber = agent.PhoneNumber
            };

            await _context.AddAsync(agentFund);
            await _context.SaveChangesAsync();

            if (result.Succeeded)
            {
                return Ok("You created " + agentRegistrationResource.FirstName + " " + agentRegistrationResource.LastName + " as a Agent");
            }

            throw new ApplicationException("UNKNOWN_ERROR");
        }

        [HttpPut]
        public async Task<object> AgentProfileUpdate([FromBody] AgentProfileResource agentProfileResource)
        {
            var webRoot = _env.WebRootPath;
            var PathWithFolderName = Path.Combine(webRoot, "Image");

            var ckPhoneNumber = _userManager.Users.Where(_ => _.PhoneNumber == agentProfileResource.MobileNumber && _.Id != agentProfileResource.CurrentUser).AsNoTracking().FirstOrDefault();
            if (ckPhoneNumber != null)
            {
                return Ok("This Phone Number is Already used");
            }
            var currentuserDetails = _userManager.Users.Where(_ => _.Id == agentProfileResource.CurrentUser).FirstOrDefault(_ => _.Id == agentProfileResource.CurrentUser);
            if (currentuserDetails != null)
            {
                currentuserDetails.FirstName = agentProfileResource.FirstName;
                currentuserDetails.LastName = agentProfileResource.LastName;
                currentuserDetails.ProfilePhoto = agentProfileResource.ProfilePhoto;
                currentuserDetails.Email = agentProfileResource.Email;
                currentuserDetails.PhoneNumber = agentProfileResource.MobileNumber;
            }
            try
            {
                var result = await _userManager.UpdateAsync(currentuserDetails);
                if (result.Succeeded)
                {
                    return Ok("You your Profile Successfully");
                }
                return Ok("Nothing to update");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        public async Task<object> ChangePassword([FromBody] PasswordChangeResource passwordChangeResource)
        {
            var currentUser = _userManager.Users.Where(_ => _.Id == passwordChangeResource.currentUserId).FirstOrDefault(_ => _.Id == passwordChangeResource.currentUserId);
            if (ModelState.IsValid)
            {
                var result = await _userManager.ChangePasswordAsync(currentUser, passwordChangeResource.CurrentPassword, passwordChangeResource.NewPassword);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View();
                }
                await _signInManager.RefreshSignInAsync(currentUser);
                return Ok("Password Change Successfully");
            }
            return Ok(passwordChangeResource);
        }


        [HttpPost]
        public async Task<object> WithdrawMoneyToOfarz([FromBody] WithdrawMoneyAgentToOfarzResource withdraw)
        {
            var currentUser = withdraw.AgentId;

            var currentUserDetails = _userManager.Users.Where(_ => _.Id == currentUser).FirstOrDefault();

            var role = _context.Roles.Where(_ => _.Id == currentUserDetails.ApplicationRoleId).FirstOrDefault();

            var agentfundExist = _context.AgentFunds.Where(_ => _.AgentId == currentUser).FirstOrDefault();


            if (agentfundExist.MainAccount < withdraw.Amount)
            {
                return Ok("You have Not enough Money For CashOut");
            }

            if (agentfundExist.MainAccount >= withdraw.Amount)
            {
                var withdrawMoney = new WithdrawMoney
                {
                    Amount = withdraw.Amount,
                    UserId = currentUser,
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

                        ofarzFundExist.GetMoneyByAgent = ofarzFundExist.GetMoneyByAgent + amount;

                        ofarzFundExist.MainAccount = ofarzFundExist.MainAccount + amount;

                        _context.Update(ofarzFundExist);
                        _context.SaveChanges();

                    }
                }

                if (findOfarz == null)
                {
                    return (currentUserDetails, role, agentfundExist);
                }

                agentfundExist.MainAccount = agentfundExist.MainAccount - amount;
                _context.Update(agentfundExist);
                _context.SaveChanges();

                await _context.AddAsync(withdrawMoney);
                await _context.SaveChangesAsync();

                return (currentUserDetails, role, agentfundExist);
            }


            return Ok();
        }


        private object GenerateJwtToken(string mobilenNmber, IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, mobilenNmber),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
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
                string revUrl = Reverse.reverse(path_to_Images);
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
                string finalString = Reverse.reverse(sub);

                string f = finalString.Replace("\\", "/");
                return f;

            }
        }
    }

    public static class Reverse
    {
        public static string reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

    }
}
