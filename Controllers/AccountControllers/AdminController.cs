using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ofarz_rest_api.Domain.Models.Account;
using ofarz_rest_api.Persistence.Context;
using ofarz_rest_api.Resources.AuthResources.LoginResources;
using ofarz_rest_api.Resources.AuthResources.ProfileResources;
using ofarz_rest_api.Resources.AuthResources.RegistrationResources;
using ofarz_rest_api.Resources.UserResources;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ofarz_rest_api.Controllers.AuthController
{
    [Route("api/[controller]/[action]")]

    public class AdminController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public AdminController(
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


        [HttpPost]
        public async Task<object> Signin([FromBody] AdminLoginResource model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.MobileNumber, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager
                              .Users
                              .Where(_ => _.PhoneNumber == model.MobileNumber)
                              .FirstOrDefault();

                //var appUser = _userManager.Users.SingleOrDefault(r => r.UserName == model.MobileNumber);
                var role = _roleManager.Roles.Where(_ => _.Id == appUser.ApplicationRoleId).FirstOrDefault();
                var token = GenerateJwtToken(model.MobileNumber, appUser);

                //var sharerfund = _context.SharerFunds.Where(_ => _.SharerId == appUser.Id).FirstOrDefault();
                //var shoperFund = _context.ShoperFunds.Where(_ => _.ShoperId == appUser.Id).FirstOrDefault();
                //var agentfund = _context.AgentFunds.Where(_ => _.AgentId == appUser.Id).FirstOrDefault();
                //var karrotfund = _context.KarrotFunds.Where(_ => _.KarrotId == appUser.Id).FirstOrDefault();
                //var ceofund = _context.CeoFunds.Where(_ => _.CeoId == appUser.Id).FirstOrDefault();
                //var ofarzFund = _context.OfarzFunds.Where(_ => _.OfarzId == appUser.Id).FirstOrDefault();

                if (appUser.ApplicationRole.Name == "AppSharer")
                {
                    return (appUser, role);
                }

                else if (appUser.ApplicationRole.Name == "Agent")
                {
                    return (appUser, role);
                }

                else if (appUser.ApplicationRole.Name == "Shoper")
                {
                    return (appUser, role);
                }

                else if (appUser.ApplicationRole.Name == "Karrot")
                {
                    return (appUser, role);
                }

                else if (appUser.ApplicationRole.Name == "CEO")
                {
                    return (appUser, role);
                }
                else
                {
                    return (appUser, role);
                }


            }

            throw new ApplicationException();
        }




        [HttpGet("{currentUserId}")]
        [Authorize(Roles = "Admin")]
        public async Task<object> AdminProfile(string currentUserId)
        {
            var profileDetails = await _userManager.Users
                                .Where(_ => _.Id == currentUserId)
                                .AsNoTracking()
                                .FirstOrDefaultAsync(_ => _.Id == currentUserId);
            return profileDetails;
        }


        [HttpPost, DisableRequestSizeLimit]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> SavePhoto()
        {
            var files = Request.Form.Files as List<IFormFile>;
            string imageUrl = ImageUrl(files[0]);
            return Ok(await Task.FromResult(imageUrl));
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<object> AdminRegistration([FromBody] AdminRegistrationResource adminRegistrationResource)
        {
            var webRoot = _env.WebRootPath;

            var PathWithFolderName = Path.Combine(webRoot, "Image");

            var adminRole = _roleManager.Roles.Where(_ => _.Name == "Admin").FirstOrDefault();

            var admin = new ApplicationUser
            {
                UserName = adminRegistrationResource.MobileNumber,
                NormalizedUserName = adminRegistrationResource.MobileNumber,
                FirstName = adminRegistrationResource.FirstName,
                LastName = adminRegistrationResource.LastName,
                Email = adminRegistrationResource.Email,
                PhoneNumber = adminRegistrationResource.MobileNumber,
                NID_Number = adminRegistrationResource.NID_Number,
                ProfilePhoto = adminRegistrationResource.ProfilePhoto,
                PostalCode = adminRegistrationResource.PostalCode,
                CountryId = adminRegistrationResource.CountryId,
                DivisionId = adminRegistrationResource.DivisionId,
                DistrictId = adminRegistrationResource.DistrictId,
                UpozilaId = adminRegistrationResource.UpozilaId,
                UnionOrWardId = adminRegistrationResource.UnionOrWardId,
                ApplicationRole = adminRole
            };
            var result = await _userManager.CreateAsync(admin, adminRegistrationResource.Password);

            if (result.Succeeded)
            {
                return Ok("You created " + adminRegistrationResource.FirstName + " as a Admin");
            }

            throw new ApplicationException("UNKNOWN_ERROR");
        }


        [HttpPut]
        public async Task<object> AdminProfileUpdate([FromBody] AdminProfileResource adminProfileResource)
        {
            var webRoot = _env.WebRootPath;
            var PathWithFolderName = Path.Combine(webRoot, "Image");

            var ckPhoneNumber = _userManager.Users.Where(_ => _.PhoneNumber == adminProfileResource.MobileNumber && _.Id != adminProfileResource.CurrentUser).AsNoTracking().FirstOrDefault();
            if (ckPhoneNumber != null)
            {
                return Ok("This Phone Number is Already used");
            }
            var currentuserDetails = _userManager.Users.Where(_ => _.Id == adminProfileResource.CurrentUser).FirstOrDefault(_ => _.Id == adminProfileResource.CurrentUser);
            if (currentuserDetails != null)
            {
                currentuserDetails.FirstName = adminProfileResource.FirstName;
                currentuserDetails.LastName = adminProfileResource.LastName;
                currentuserDetails.ProfilePhoto = adminProfileResource.ProfilePhoto;
                currentuserDetails.Email = adminProfileResource.Email;
                currentuserDetails.PhoneNumber = adminProfileResource.MobileNumber;
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
        //[Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<object> ChangePassword([FromBody] PasswordChangeResource passwordChangeResource)
        {
            var appUser = _userManager.Users.Where(_ => _.Id == passwordChangeResource.currentUserId).FirstOrDefault(_ => _.Id == passwordChangeResource.currentUserId);
            if (ModelState.IsValid)
            {
                var result = await _userManager.ChangePasswordAsync(appUser, passwordChangeResource.CurrentPassword, passwordChangeResource.NewPassword);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View();
                }

                await _signInManager.RefreshSignInAsync(appUser);

                var role = _roleManager.Roles.Where(_ => _.Id == appUser.ApplicationRoleId).AsNoTracking().FirstOrDefault();


                if (appUser.ApplicationRole.Name == "AppSharer")
                {
                    return (appUser, role);
                }

                else if (appUser.ApplicationRole.Name == "Agent")
                {
                    return (appUser, role);
                }

                else if (appUser.ApplicationRole.Name == "Shoper")
                {
                    return (appUser, role);
                }

                else if (appUser.ApplicationRole.Name == "Karrot")
                {
                    return (appUser, role);
                }

                else if (appUser.ApplicationRole.Name == "CEO")
                {
                    return (appUser, role);
                }
                else
                {
                    return (appUser, role);
                }



            }
            throw new ApplicationException();
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
