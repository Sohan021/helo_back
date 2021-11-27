using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ofarz_rest_api.Domain.Models.Account;
using ofarz_rest_api.Resources.AuthResources.LoginResources;
using ofarz_rest_api.Resources.AuthResources.RegistrationResources;
using ofarz_rest_api.Resources.AuthResources.UserDeleteResources;
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
    public class ModeratorController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _configuration;

        public ModeratorController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            IHostingEnvironment env,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _env = env;
            _configuration = configuration;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllModerator()
        {
            var moderators = await _userManager.Users
                .Where(_ => _.ApplicationRole.Name == "Moderator").ToListAsync();

            return Ok(moderators);
        }


        [HttpPost]
        public async Task<object> ModeratorSignin([FromBody] ModeratorLoginResource model)
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
        public async Task<object> AddModerator([FromBody] ModeratorRegistrationResource moderatorRegistrationResource)
        {
            var webRoot = _env.WebRootPath;
            var PathWithFolderName = Path.Combine(webRoot, "Image");
            var adminRole = _roleManager.Roles.Where(_ => _.Name == "Moderator").FirstOrDefault();

            var admin = new ApplicationUser
            {
                UserName = moderatorRegistrationResource.MobileNumber,
                NormalizedUserName = moderatorRegistrationResource.MobileNumber,
                FirstName = moderatorRegistrationResource.FirstName,
                LastName = moderatorRegistrationResource.LastName,
                PhoneNumber = moderatorRegistrationResource.MobileNumber,
                NID_Number = moderatorRegistrationResource.NID_Number,
                ProfilePhoto = moderatorRegistrationResource.ProfilePhoto,
                PostalCode = moderatorRegistrationResource.PostalCode,
                ApplicationRole = adminRole
            };
            var result = await _userManager.CreateAsync(admin, moderatorRegistrationResource.Password);

            if (result.Succeeded)
            {
                return Ok("You created " + moderatorRegistrationResource.FirstName + moderatorRegistrationResource.LastName + " as a Moderator");
            }

            throw new ApplicationException("UNKNOWN_ERROR");
        }

        [HttpDelete]
        public async Task<object> DeleteModerator([FromBody] ModeratorDeleteResource moderatorDeleteResource)
        {
            var moderator = _userManager.Users
                .Where(_ => _.PhoneNumber == moderatorDeleteResource.PhoneNumber)
                .Where(_ => _.ApplicationRole.Name == "Moderator")
                .FirstOrDefault();

            var result = await _userManager.DeleteAsync(moderator);

            if (result.Succeeded)
            {
                return Ok("Deteted Moderator Successfully");
            }

            throw new ApplicationException("UNKNOWN_ERROR");
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
                string revUrl = ModeratorReverse.moderatorreverse(path_to_Images);
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
                string finalString = ModeratorReverse.moderatorreverse(sub);

                string f = finalString.Replace("\\", "/");
                return f;

            }


        }
    }

    public static class ModeratorReverse
    {
        public static string moderatorreverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

    }
}
