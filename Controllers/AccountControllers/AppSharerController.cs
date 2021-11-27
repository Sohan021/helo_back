using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ofarz_rest_api.core.Infrastructure;
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
using static ofarz_rest_api.core.Infrastructure.Constants;

namespace ofarz_rest_api.Controllers.AccountControllers
{
    [Route("api/[controller]/[action]")]
    public class AppSharerController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _configuration;

        private readonly AppDbContext _context;

        public AppSharerController(
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
        public async Task<IActionResult> GetAllAppSharer()
        {
            var appSharers = await _userManager.Users
                .Where(_ => _.ApplicationRole.Name == Roles.AppSharer).ToListAsync();

            return Ok(appSharers);
        }


        [HttpGet("{currentUserId}")]
        public async Task<object> GetProfileDetails(string currentUserId)
        {
            var appSharer = await _userManager.Users
                .Where(_ => _.Id == currentUserId)
                .Include(_ => _.Reffrer)
                .FirstOrDefaultAsync();

            return appSharer;
        }


        [HttpGet("{id}")]
        public async Task<object> GetFullDownlineCount(string id)
        {

            var downlineCount = await _userManager.Users.Where(_ => _.Id == id).AsNoTracking().FirstOrDefaultAsync();


            return (downlineCount);
        }


        [HttpGet("{id}")]
        public async Task<object> GetDownlineList(string id)
        {
            var downLineList = await _userManager.Users
                .Where(_ => _.ApplicationRole.Name == Roles.AppSharer)
                .Where(_ => _.ReffrerId == id)
                .Include(_ => _.Reffrer)
                .ToListAsync();



            var downlineCount = _userManager.Users
                               .Where(_ => _.ReffrerId == id
                               || _.Reffrer.ReffrerId == id
                               || _.Reffrer.Reffrer.ReffrerId == id
                               || _.Reffrer.Reffrer.Reffrer.ReffrerId == id
                               || _.Reffrer.Reffrer.Reffrer.Reffrer.ReffrerId == id).AsNoTracking()
                               .Count();


            return (downLineList);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUpline(string id)
        {
            var downLineList = await _userManager.Users
                .Where(_ => _.Id == id)
                .Where(_ => _.ApplicationRole.Name == Roles.AppSharer)
                .Include(_ => _.Reffrer)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return Ok(downLineList);
        }


        [HttpPost]
        public async Task<object> Login([FromBody] AppSharerLoginResource model)
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

        [HttpPost]
        public async Task<object> AddDownline([FromBody] AppSharerRegistrationResource resource)
        {

            var currentUserId = resource.CurrentUser;

            var role = _roleManager.Roles.Where(r => r.Name == Roles.AppSharer).FirstOrDefault();

            var firstUpline = _userManager.Users.Where(_ => _.Id == currentUserId).FirstOrDefault();

            if (firstUpline.ApplicationRole.Name == Roles.AppSharer)
            {
                var downLineCount = _userManager.Users.Where(_ => _.ReffrerId == currentUserId).Count();

                if (downLineCount < 5)
                {
                    var sharer = new ApplicationUser
                    {
                        UserName = resource.MobileNumber,
                        NormalizedUserName = resource.MobileNumber,
                        PhoneNumber = resource.MobileNumber,
                        NID_Number = resource.NID_Number,
                        Reffrer = firstUpline,
                        DownlineCount = 0,
                        ReffrerName = firstUpline.FirstName + firstUpline.LastName,
                        ApplicationRole = role,
                    };
                    var result = await _userManager.CreateAsync(sharer, resource.Password);

                    var sharerFund = new SharerFund
                    {
                        SharerId = sharer.Id,
                        MainAccount = 0.0,
                        BackShoppingAccount = 0.0,
                        SharerName = sharer.FirstName,
                        SharerPhoneNumber = sharer.PhoneNumber

                    };

                    await _context.AddAsync(sharerFund);

                    await _context.SaveChangesAsync();

                    if (result.Succeeded)
                    {
                        firstUpline.DownlineCount = firstUpline.DownlineCount + 1;
                        firstUpline.FirstLevelDownlineCount = firstUpline.FirstLevelDownlineCount + 1;
                        await _userManager.UpdateAsync(firstUpline);


                        var secondUpline = _userManager.Users.Where(_ => _.Id == firstUpline.ReffrerId).FirstOrDefault();
                        secondUpline.DownlineCount = secondUpline.DownlineCount + 1;
                        secondUpline.SecondLevelDownlineCount = secondUpline.SecondLevelDownlineCount + 1;
                        await _userManager.UpdateAsync(secondUpline);


                        var thirdUpline = _userManager.Users.Where(_ => _.Id == secondUpline.ReffrerId).FirstOrDefault();
                        thirdUpline.DownlineCount = thirdUpline.DownlineCount + 1;
                        thirdUpline.ThirdLevelDownlineCount = thirdUpline.ThirdLevelDownlineCount + 1;
                        await _userManager.UpdateAsync(thirdUpline);


                        var fourthUpline = _userManager.Users.Where(_ => _.Id == thirdUpline.ReffrerId).FirstOrDefault();
                        fourthUpline.DownlineCount = fourthUpline.DownlineCount + 1;
                        fourthUpline.FourthLevelDownlineCount = fourthUpline.FourthLevelDownlineCount + 1;
                        await _userManager.UpdateAsync(fourthUpline);


                        var fifthUpline = _userManager.Users.Where(_ => _.Id == fourthUpline.ReffrerId).FirstOrDefault();
                        fifthUpline.DownlineCount = fifthUpline.DownlineCount + 1;
                        fifthUpline.FifthLevelDownlineCount = fifthUpline.FifthLevelDownlineCount + 1;
                        await _userManager.UpdateAsync(fifthUpline);


                        return (firstUpline, role);
                    }
                }
                if (downLineCount >= 5)
                {
                    return Ok("Your Dowline Limition is over");
                }

            }

            throw new ApplicationException("UNKNOWN_ERROR");
        }

        [HttpPost, DisableRequestSizeLimit]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> SavePhoto()
        {
            var files = Request.Form.Files as List<IFormFile>;
            string imageUrl = ImageUrl(files[0]);
            return Ok(await Task.FromResult(imageUrl));
        }

        [HttpPut("{currentUser}")]
        public async Task<object> AppSharerProfileUpdate([FromBody] AppSharerProfileResource appSharerProfileResource)
        {
            var webRoot = _env.WebRootPath;
            var PathWithFolderName = Path.Combine(webRoot, "Image");

            var currentuserDetails = await _userManager
                                    .Users
                                    .Where(_ => _.Id == appSharerProfileResource.CurrentUser)
                                    .FirstOrDefaultAsync();

            var role = await _roleManager
                        .Roles
                        .Where(_ => _.Id == currentuserDetails.ApplicationRoleId)
                        .FirstOrDefaultAsync();

            if (currentuserDetails != null)
            {
                currentuserDetails.FirstName = appSharerProfileResource.FirstName;
                currentuserDetails.LastName = appSharerProfileResource.LastName;
                currentuserDetails.Email = appSharerProfileResource.Email;
                currentuserDetails.ProfilePhoto = appSharerProfileResource.ProfilePhoto;
                currentuserDetails.PostalCode = appSharerProfileResource.PostalCode;
                currentuserDetails.Nominee_PhonNumber = appSharerProfileResource.Nominee_PhonNumber;
                currentuserDetails.Nominee_Name = appSharerProfileResource.Nominee_Name;
                currentuserDetails.Nominee_Relation = appSharerProfileResource.Nominee_Relation;
                currentuserDetails.CountryId = appSharerProfileResource.CountryId;
                currentuserDetails.DivisionId = appSharerProfileResource.DivisionId;
                currentuserDetails.DistrictId = appSharerProfileResource.DistrictId;
                currentuserDetails.UpozilaId = appSharerProfileResource.UpozilaId;
                currentuserDetails.UnionOrWardId = appSharerProfileResource.UnionOrWardId;
                currentuserDetails.MarketId = appSharerProfileResource.MarketId;

                var country = _context.Countries.Where(_ => _.Id == appSharerProfileResource.CountryId).FirstOrDefault();
                var division = _context.Divisions.Where(_ => _.Id == appSharerProfileResource.DivisionId).FirstOrDefault();
                var district = _context.Districts.Where(_ => _.Id == appSharerProfileResource.DistrictId).FirstOrDefault();
                var upozila = _context.Upozillas.Where(_ => _.Id == appSharerProfileResource.UpozilaId).FirstOrDefault();
                var union = _context.UnionOrWards.Where(_ => _.Id == appSharerProfileResource.UnionOrWardId).FirstOrDefault();

                currentuserDetails.CountryName = country.Name;
                currentuserDetails.DivisionName = division.Name;
                currentuserDetails.DistrictName = district.Name;
                currentuserDetails.UpozilaName = upozila.Name;
                currentuserDetails.UnionOrWardName = union.Name;

            }
            try
            {
                var result = await _userManager.UpdateAsync(currentuserDetails);
                if (result.Succeeded)
                {
                    return (currentuserDetails, role);
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
        public async Task<object> PaymentSubmitTableCashOffer([FromBody] Payment payment)
        {


            var currentUserId = payment.PayerId;

            var currentUserDetails = _userManager.Users.Where(_ => _.Id == currentUserId).FirstOrDefault();

            var productType = _context.ProductTypes.Where(_ => _.Name == ProductType.Offer).FirstOrDefault();

            var role = await _roleManager.Roles.Where(_ => _.Name == Roles.AppSharer).FirstOrDefaultAsync();

            var paymentType = _context.PaymentTypes.Where(_ => _.PaymentTypeName == Constants.PaymentType.TableCashOffer).FirstOrDefault();

            var pay = new Payment
            {
                Id = payment.Id,
                Amount = payment.Amount,
                PayerId = payment.PayerId,
                PaymentTime = DateTime.Now,
                AgentPhnNumber = payment.AgentPhnNumber,
                PayerName = currentUserDetails.FirstName,
                PayerPhoneNumber = currentUserDetails.PhoneNumber,
                ProductType = productType,
                PaymentType = paymentType
            };

            var TotalAmount = payment.Amount;

            var UplineAmount = (TotalAmount * .90) / 100;

            var backshoppingAmount = (TotalAmount * 50.00 / 100);

            var karrotAmount = ((TotalAmount * .10) / 100) * 5;

            //var ceoAmount = ((TotalAmount * .02) / 100) * 5;
            var ceoAmount = 0;




            var findAgent = _userManager.Users
                               .Where(_ => _.PhoneNumber == payment.AgentPhnNumber)
                               .FirstOrDefault();

            if (findAgent != null)
            {
                var agentFundExist = _context.AgentFunds.Where(_ => _.AgentId == findAgent.Id).FirstOrDefault();



                if (agentFundExist != null)
                {

                    agentFundExist.SellViaDirectCash = agentFundExist.SellViaDirectCash + TotalAmount;

                    agentFundExist.TotalTransection = agentFundExist.TotalTransection + TotalAmount;

                    _context.Update(agentFundExist);
                    _context.SaveChanges();

                }
            }

            var findReferer = _context.ApplicationUsers
                                .Where(_ => _.Id == currentUserId)
                                .Include(_ => _.Reffrer)
                                .ThenInclude(_ => _.Reffrer.Reffrer)
                                .ThenInclude(_ => _.Reffrer.Reffrer.Reffrer)
                                .ThenInclude(_ => _.Reffrer.Reffrer.Reffrer.Reffrer)
                                .ThenInclude(_ => _.Reffrer.Reffrer.Reffrer.Reffrer.Reffrer)
                                .FirstOrDefault();

            var firstUpline = findReferer.Reffrer;

            if (firstUpline != null)
            {
                var firstUplineAccount = _context.SharerFunds.Where(_ => _.SharerId == firstUpline.Id).FirstOrDefault();

                if (firstUplineAccount != null)
                {
                    firstUplineAccount.MainAccount = firstUplineAccount.MainAccount + UplineAmount;
                    _context.Update(firstUplineAccount);
                    await _context.SaveChangesAsync();
                }


                var secondUpline = firstUpline.Reffrer;

                if (secondUpline != null)
                {
                    var secondUplineAccount = _context.SharerFunds.Where(_ => _.SharerId == secondUpline.Id).FirstOrDefault();



                    if (secondUplineAccount != null)
                    {
                        secondUplineAccount.MainAccount = secondUplineAccount.MainAccount + UplineAmount;
                        _context.Update(secondUplineAccount);
                        _context.SaveChanges();
                    }


                    var thirdUpline = firstUpline.Reffrer.Reffrer;

                    if (thirdUpline != null)
                    {
                        var thirdUplineAccount = _context.SharerFunds.Where(_ => _.SharerId == thirdUpline.Id).FirstOrDefault();



                        if (thirdUplineAccount != null)
                        {
                            thirdUplineAccount.MainAccount = thirdUplineAccount.MainAccount + UplineAmount;
                            _context.Update(thirdUplineAccount);
                            _context.SaveChanges();
                        }



                        var fourthUpline = firstUpline.Reffrer.Reffrer.Reffrer;

                        if (fourthUpline != null)
                        {
                            var fourthUplineAccount = _context.SharerFunds.Where(_ => _.SharerId == fourthUpline.Id).FirstOrDefault();



                            if (fourthUplineAccount != null)
                            {
                                fourthUplineAccount.MainAccount = fourthUplineAccount.MainAccount + UplineAmount;
                                _context.Update(fourthUplineAccount);
                                _context.SaveChanges();
                            }


                            var fifthUpline = firstUpline.Reffrer.Reffrer.Reffrer.Reffrer;

                            if (fifthUpline != null)
                            {
                                var fifthUplineAccount = _context.SharerFunds.Where(_ => _.SharerId == fifthUpline.Id).FirstOrDefault();



                                if (fifthUplineAccount != null)
                                {
                                    fifthUplineAccount.MainAccount = fifthUplineAccount.MainAccount + UplineAmount;

                                    _context.Update(fifthUplineAccount);
                                    _context.SaveChanges();
                                }
                            }
                        }
                    }
                }
            }

            var userAccount = _context.SharerFunds.Where(_ => _.SharerId == currentUserId).FirstOrDefault();

            if (userAccount != null)
            {
                userAccount.SharerId = currentUserId;
                userAccount.BackShoppingAccount = (userAccount.BackShoppingAccount + ((50.00 * TotalAmount) / 100.0));
                userAccount.SharerName = currentUserDetails.FirstName + " " + currentUserDetails.LastName;
                userAccount.SharerPhoneNumber = currentUserDetails.PhoneNumber;

                _context.Update(userAccount);
                _context.SaveChanges();
            }

            var karrotAccount = await _context.KarrotFunds.Where(_ => _.KarrotCode == 21873465).FirstOrDefaultAsync();

            if (karrotAccount != null)
            {
                karrotAccount.MainAccount = karrotAccount.MainAccount + karrotAmount;
                karrotAccount.TotalIncome = karrotAccount.TotalIncome + karrotAmount;
                _context.Update(karrotAccount);
                await _context.SaveChangesAsync();

            }

            var ceoAccount = await _context.CeoFunds.Where(_ => _.CeoCode == 98327489).FirstOrDefaultAsync();

            if (ceoAccount != null)
            {
                ceoAccount.MainAccount = ceoAccount.MainAccount + ceoAmount;
                ceoAccount.MainAccount = ceoAccount.MainAccount + ceoAmount;
                _context.Update(ceoAccount);
                await _context.SaveChangesAsync();

            }

            await _context.AddAsync(pay);
            await _context.SaveChangesAsync();

            return (currentUserDetails, role);
        }

        [HttpPost]
        public async Task<object> PaymentSubmitTableCashPromotional([FromBody] Payment payment)
        {
            var currentUserId = payment.PayerId;

            var currentUserDetails = _userManager.Users.Where(_ => _.Id == currentUserId).FirstOrDefault();

            var role = await _context.Roles.Where(_ => _.Id == currentUserDetails.ApplicationRoleId).FirstOrDefaultAsync();

            var productType = _context.ProductTypes.Where(_ => _.Name == "Promotional").FirstOrDefault();

            var paymentType = _context.PaymentTypes.Where(_ => _.PaymentTypeName == "TableCashPromotional").FirstOrDefault();

            var pay = new Payment
            {
                Id = payment.Id,
                Amount = payment.Amount,
                PayerId = currentUserId,
                PaymentTime = DateTime.Now,
                AgentPhnNumber = payment.AgentPhnNumber,
                PayerName = currentUserDetails.FirstName,
                PayerPhoneNumber = currentUserDetails.PhoneNumber,
                ProductType = productType,
                PaymentType = paymentType
            };



            var TotalAmount = payment.Amount;

            var UplineAmount = (TotalAmount * 2.70) / 100;

            var karrotAmount = ((TotalAmount * .30) / 100) * 5;

            //var ceoAmount = ((TotalAmount * .06) / 100) * 5;
            var ceoAmount = 0;


            var findAgent = _userManager.Users
                               .Where(_ => _.PhoneNumber == payment.AgentPhnNumber)
                               .FirstOrDefault();

            if (findAgent != null)
            {
                var agentFundExist = _context.AgentFunds.Where(_ => _.AgentId == findAgent.Id).FirstOrDefault();



                if (agentFundExist != null)
                {

                    agentFundExist.SellViaDirectCash = agentFundExist.SellViaDirectCash + TotalAmount;

                    agentFundExist.TotalTransection = agentFundExist.TotalTransection + TotalAmount;

                    _context.Update(agentFundExist);
                    _context.SaveChanges();

                }
            }



            var findReferer = _context.ApplicationUsers
                                .Where(_ => _.Id == currentUserId)
                                .Include(_ => _.Reffrer)
                                .ThenInclude(_ => _.Reffrer.Reffrer)
                                .ThenInclude(_ => _.Reffrer.Reffrer.Reffrer)
                                .ThenInclude(_ => _.Reffrer.Reffrer.Reffrer.Reffrer)
                                .ThenInclude(_ => _.Reffrer.Reffrer.Reffrer.Reffrer.Reffrer)
                                .FirstOrDefault();

            var firstUpline = findReferer.Reffrer;

            if (firstUpline != null)
            {
                var firstUplineAccount = _context.SharerFunds.Where(_ => _.SharerId == firstUpline.Id).FirstOrDefault();



                if (firstUplineAccount != null)
                {
                    firstUplineAccount.MainAccount = firstUplineAccount.MainAccount + UplineAmount;
                    _context.Update(firstUplineAccount);
                    await _context.SaveChangesAsync();
                }


                var secondUpline = firstUpline.Reffrer;

                if (secondUpline != null)
                {
                    var secondUplineAccount = _context.SharerFunds.Where(_ => _.SharerId == secondUpline.Id).FirstOrDefault();



                    if (secondUplineAccount != null)
                    {
                        secondUplineAccount.MainAccount = secondUplineAccount.MainAccount + UplineAmount;
                        _context.Update(secondUplineAccount);
                        _context.SaveChanges();
                    }




                    var thirdUpline = firstUpline.Reffrer.Reffrer;

                    if (thirdUpline != null)
                    {
                        var thirdUplineAccount = _context.SharerFunds.Where(_ => _.SharerId == thirdUpline.Id).FirstOrDefault();



                        if (thirdUplineAccount != null)
                        {
                            thirdUplineAccount.MainAccount = thirdUplineAccount.MainAccount + UplineAmount;
                            _context.Update(thirdUplineAccount);
                            _context.SaveChanges();
                        }



                        var fourthUpline = firstUpline.Reffrer.Reffrer.Reffrer;

                        if (fourthUpline != null)
                        {
                            var fourthUplineAccount = _context.SharerFunds.Where(_ => _.SharerId == fourthUpline.Id).FirstOrDefault();



                            if (fourthUplineAccount != null)
                            {
                                fourthUplineAccount.MainAccount = fourthUplineAccount.MainAccount + UplineAmount;
                                _context.Update(fourthUplineAccount);
                                _context.SaveChanges();
                            }


                            var fifthUpline = firstUpline.Reffrer.Reffrer.Reffrer.Reffrer;

                            if (fifthUpline != null)
                            {
                                var fifthUplineAccount = _context.SharerFunds.Where(_ => _.SharerId == fifthUpline.Id).FirstOrDefault();



                                if (fifthUplineAccount != null)
                                {
                                    fifthUplineAccount.MainAccount = fifthUplineAccount.MainAccount + UplineAmount;
                                    _context.Update(fifthUplineAccount);
                                    _context.SaveChanges();
                                }
                            }
                        }
                    }
                }
            }



            var sharerPaymentListBeforeThisPayment = _context.Payments
                                .Where(_ => _.PayerId == currentUserId)
                                .Where(_ => _.PaymentType.PaymentTypeName == "TableCashPromotional" || _.PaymentType.PaymentTypeName == "MainAccount")
                                .Where(_ => _.ProductType.Name == "Promotional")
                                .ToList();

            double sharerPaymentCountBeforeThisPayment = sharerPaymentListBeforeThisPayment.Select(_ => _.Amount).Sum();

            await _context.AddAsync(pay);
            await _context.SaveChangesAsync();

            var sharerPaymentList = _context.Payments
                                    .Where(_ => _.PayerId == currentUserId)
                                    .Where(_ => _.PaymentType.PaymentTypeName == "TableCashPromotional" || _.PaymentType.PaymentTypeName == "MainAccount")
                                    .Where(_ => _.ProductType.Name == "Promotional")
                                    .ToList();

            double sharerPaymentCount = sharerPaymentList.Select(_ => _.Amount).Sum();

            var check = (TotalAmount + sharerPaymentCountBeforeThisPayment);

            var userAccount = _context.SharerFunds.Where(_ => _.SharerId == currentUserId).FirstOrDefault();


            if (sharerPaymentList.Count == 1 && TotalAmount == sharerPaymentCount && sharerPaymentCount > 1000)
            {




                if (userAccount != null)
                {
                    //userAccount.SharerId = currentUser;
                    userAccount.BackShoppingAccount = (userAccount.BackShoppingAccount + ((25.0 * (sharerPaymentCount - 1000) / 100.0)));

                    _context.Update(userAccount);
                    _context.SaveChanges();
                }
            }






            if (sharerPaymentList.Count > 1 && sharerPaymentCountBeforeThisPayment <= 1000)
            {


                if (check > 1000)
                {


                    if (userAccount == null)
                    {
                        var userBackShoppingFund = new SharerFund
                        {
                            SharerId = currentUserId,
                            BackShoppingAccount = ((25.0 * (check - 1000)) / 100.0),
                            SharerName = currentUserDetails.FirstName + currentUserDetails.LastName
                        };

                        _context.Add(userBackShoppingFund);
                        _context.SaveChanges();
                    }

                    if (userAccount != null)
                    {
                        userAccount.SharerId = currentUserId;
                        userAccount.BackShoppingAccount = (userAccount.BackShoppingAccount + ((25.0 * (check - 1000) / 100.0)));

                        _context.Update(userAccount);
                        _context.SaveChanges();
                    }
                }


            }

            if (sharerPaymentCountBeforeThisPayment > 1000)
            {



                if (userAccount == null)
                {
                    var userBackShoppingFund = new SharerFund
                    {
                        SharerId = currentUserId,
                        BackShoppingAccount = ((25.0 * TotalAmount) / 100.0),
                        SharerName = currentUserDetails.FirstName + currentUserDetails.LastName
                    };

                    _context.Add(userBackShoppingFund);
                    _context.SaveChanges();
                }

                if (userAccount != null)
                {
                    userAccount.SharerId = currentUserId;
                    userAccount.BackShoppingAccount = (userAccount.BackShoppingAccount + ((25.0 * TotalAmount) / 100.0));

                    _context.Update(userAccount);
                    _context.SaveChanges();
                }
            }

            var karrotAccount = await _context.KarrotFunds.Where(_ => _.KarrotCode == 21873465).FirstOrDefaultAsync();

            if (karrotAccount != null)
            {
                karrotAccount.MainAccount = karrotAccount.MainAccount + karrotAmount;
                karrotAccount.MainAccount = karrotAccount.MainAccount + karrotAmount;
                _context.Update(karrotAccount);
                await _context.SaveChangesAsync();
            }

            var ceoAccount = await _context.CeoFunds.Where(_ => _.CeoCode == 98327489).FirstOrDefaultAsync();

            if (ceoAccount != null)
            {
                ceoAccount.MainAccount = ceoAccount.MainAccount + ceoAmount;
                ceoAccount.TotalIncome = ceoAccount.TotalIncome + ceoAmount;
                _context.Update(ceoAccount);
                await _context.SaveChangesAsync();

            }

            return (currentUserDetails, role);
        }

        [HttpPost]
        public async Task<object> PaymentSubmitMainAccounntOffer([FromBody] Payment payment)
        {
            var currentUser = payment.PayerId;

            var currentUserDetails = _userManager.Users.Where(_ => _.Id == currentUser).FirstOrDefault();

            var role = await _context.Roles.Where(_ => _.Id == currentUserDetails.ApplicationRoleId).FirstOrDefaultAsync();

            var productType = _context.ProductTypes.Where(_ => _.Name == "Offer").FirstOrDefault();

            var fundExist = _context.SharerFunds.Where(_ => _.SharerId == currentUser).FirstOrDefault();

            var paymentType = _context.PaymentTypes.Where(_ => _.PaymentTypeName == "MainAccountOffer").FirstOrDefault();

            if (fundExist.MainAccount < payment.Amount)
            {
                return Ok("You have not enough money at Main Account");
            }

            if (fundExist.MainAccount >= payment.Amount)
            {
                var pay = new Payment
                {
                    Amount = payment.Amount,
                    PayerId = currentUser,
                    PaymentTime = DateTime.Now,
                    AgentPhnNumber = payment.AgentPhnNumber,
                    PayerName = currentUserDetails.FirstName,
                    PayerPhoneNumber = currentUserDetails.PhoneNumber,
                    ProductType = productType,
                    PaymentType = paymentType
                };

                var TotalAmount = payment.Amount;

                var UplineAmount = (TotalAmount * .90) / 100;

                var backshoppingAmount = (TotalAmount * 50.00 / 100);

                var karrotAmount = ((TotalAmount * .10) / 100) * 5;

                //var ceoAmount = ((TotalAmount * .02) / 100) * 5;
                var ceoAmount = 0;



                var findAgent = _userManager.Users
                                .Where(_ => _.PhoneNumber == payment.AgentPhnNumber)

                                .FirstOrDefault();

                if (findAgent != null)
                {
                    var agentFundExist = _context.AgentFunds.Where(_ => _.AgentId == findAgent.Id).FirstOrDefault();


                    if (agentFundExist != null)
                    {

                        agentFundExist.MainAccount = agentFundExist.MainAccount + TotalAmount;

                        agentFundExist.TotalTransection = agentFundExist.TotalTransection + TotalAmount;

                        _context.Update(agentFundExist);
                        _context.SaveChanges();

                    }
                }


                fundExist.MainAccount = fundExist.MainAccount - TotalAmount;
                _context.Update(fundExist);
                _context.SaveChanges();

                var findReferer = _context.ApplicationUsers
                                .Where(_ => _.Id == currentUser)
                                .Include(_ => _.Reffrer)
                                .ThenInclude(_ => _.Reffrer.Reffrer)
                                .ThenInclude(_ => _.Reffrer.Reffrer.Reffrer)
                                .ThenInclude(_ => _.Reffrer.Reffrer.Reffrer.Reffrer)
                                .ThenInclude(_ => _.Reffrer.Reffrer.Reffrer.Reffrer.Reffrer)
                                .FirstOrDefault();

                var firstUpline = findReferer.Reffrer;

                if (firstUpline != null)
                {
                    var firstUplineAccount = _context.SharerFunds.Where(_ => _.SharerId == firstUpline.Id).FirstOrDefault();



                    if (firstUplineAccount != null)
                    {
                        firstUplineAccount.MainAccount = firstUplineAccount.MainAccount + UplineAmount;
                        _context.Update(firstUplineAccount);
                        await _context.SaveChangesAsync();
                    }


                    var secondUpline = firstUpline.Reffrer;

                    if (secondUpline != null)
                    {
                        var secondUplineAccount = _context.SharerFunds.Where(_ => _.SharerId == secondUpline.Id).FirstOrDefault();



                        if (secondUplineAccount != null)
                        {
                            secondUplineAccount.MainAccount = secondUplineAccount.MainAccount + UplineAmount;
                            _context.Update(secondUplineAccount);
                            _context.SaveChanges();
                        }




                        var thirdUpline = firstUpline.Reffrer.Reffrer;

                        if (thirdUpline != null)
                        {
                            var thirdUplineAccount = _context.SharerFunds.Where(_ => _.SharerId == thirdUpline.Id).FirstOrDefault();



                            if (thirdUplineAccount != null)
                            {
                                thirdUplineAccount.MainAccount = thirdUplineAccount.MainAccount + UplineAmount;
                                _context.Update(thirdUplineAccount);
                                _context.SaveChanges();
                            }



                            var fourthUpline = firstUpline.Reffrer.Reffrer.Reffrer;

                            if (fourthUpline != null)
                            {
                                var fourthUplineAccount = _context.SharerFunds.Where(_ => _.SharerId == fourthUpline.Id).FirstOrDefault();



                                if (fourthUplineAccount != null)
                                {
                                    fourthUplineAccount.MainAccount = fourthUplineAccount.MainAccount + UplineAmount;
                                    _context.Update(fourthUplineAccount);
                                    _context.SaveChanges();
                                }


                                var fifthUpline = firstUpline.Reffrer.Reffrer.Reffrer.Reffrer;

                                if (fifthUpline != null)
                                {
                                    var fifthUplineAccount = _context.SharerFunds.Where(_ => _.SharerId == fifthUpline.Id).FirstOrDefault();



                                    if (fifthUplineAccount != null)
                                    {
                                        fifthUplineAccount.MainAccount = fifthUplineAccount.MainAccount + UplineAmount;
                                        _context.Update(fifthUplineAccount);
                                        _context.SaveChanges();
                                    }
                                }
                            }
                        }
                    }
                }

                var userAccount = _context.SharerFunds.Where(_ => _.SharerId == currentUser).FirstOrDefault();



                if (userAccount != null)
                {
                    userAccount.SharerId = currentUser;
                    userAccount.BackShoppingAccount = (userAccount.BackShoppingAccount + ((50.00 * TotalAmount) / 100.0));

                    _context.Update(userAccount);
                    _context.SaveChanges();
                }






                var karrotAccount = await _context.KarrotFunds.Where(_ => _.KarrotCode == 21873465).FirstOrDefaultAsync();

                if (karrotAccount != null)
                {
                    karrotAccount.MainAccount = karrotAccount.MainAccount + karrotAmount;
                    karrotAccount.TotalIncome = karrotAccount.TotalIncome + karrotAmount;
                    _context.Update(karrotAccount);
                    await _context.SaveChangesAsync();

                }

                var ceoAccount = await _context.CeoFunds.Where(_ => _.CeoCode == 98327489).FirstOrDefaultAsync();

                if (ceoAccount != null)
                {
                    ceoAccount.MainAccount = ceoAccount.MainAccount + ceoAmount;
                    ceoAccount.TotalIncome = ceoAccount.TotalIncome + ceoAmount;
                    _context.Update(ceoAccount);
                    await _context.SaveChangesAsync();

                }

                await _context.AddAsync(pay);
                await _context.SaveChangesAsync();


                return (currentUserDetails, role);


            }





            return Ok();
        }

        [HttpPost]
        public async Task<object> PaymentSubmitMainAccounntPromotional([FromBody] Payment payment)
        {
            var currentUserId = payment.PayerId;

            var currentUserDetails = _userManager.Users.Where(_ => _.Id == currentUserId).FirstOrDefault();

            var role = await _context.Roles.Where(_ => _.Id == currentUserDetails.ApplicationRoleId).FirstOrDefaultAsync();

            var productType = _context.ProductTypes.Where(_ => _.Name == "Promotional").FirstOrDefault();

            var fundExist = _context.SharerFunds.Where(_ => _.SharerId == currentUserId).FirstOrDefault();

            var paymentType = _context.PaymentTypes.Where(_ => _.PaymentTypeName == "MainAccountPromotional").FirstOrDefault();

            if (fundExist.MainAccount < payment.Amount)
            {
                return Ok("You have not enough money at Main Account");
            }


            // if current user has enough money to make payment, "IN MAIN ACCOUNT"
            if (fundExist.MainAccount >= payment.Amount)
            {
                var pay = new Payment
                {
                    Amount = payment.Amount,
                    PayerId = currentUserId,
                    PaymentTime = DateTime.Now,
                    AgentPhnNumber = payment.AgentPhnNumber,
                    PayerName = currentUserDetails.FirstName,
                    PayerPhoneNumber = currentUserDetails.PhoneNumber,
                    ProductType = productType,
                    PaymentType = paymentType
                };

                var TotalAmount = payment.Amount;

                var UplineAmount = (TotalAmount * 2.70) / 100;

                var karrotAmount = ((TotalAmount * .30) / 100) * 5;

                var ceoAmount = 0;
                //var ceoAmount = ((TotalAmount * .06) / 100) * 5;



                var findAgent = _userManager.Users
                                .Where(_ => _.PhoneNumber == payment.AgentPhnNumber)
                                .FirstOrDefault();


                if (findAgent != null)
                {
                    var agentFundExist = _context.AgentFunds.Where(_ => _.AgentId == findAgent.Id).FirstOrDefault();


                    if (agentFundExist != null)
                    {
                        agentFundExist.MainAccount = agentFundExist.MainAccount + TotalAmount;

                        agentFundExist.TotalTransection = agentFundExist.TotalTransection + TotalAmount;

                        _context.Update(agentFundExist);

                    }
                    _context.SaveChanges();
                }

                //now update(reduce) money from main account
                fundExist.MainAccount = fundExist.MainAccount - TotalAmount;
                _context.Update(fundExist);
                _context.SaveChanges();




                //find current users
                var findReferer = _context.ApplicationUsers
                                .Where(_ => _.Id == currentUserId)
                                .Include(_ => _.Reffrer)
                                .ThenInclude(_ => _.Reffrer.Reffrer)
                                .ThenInclude(_ => _.Reffrer.Reffrer.Reffrer)
                                .ThenInclude(_ => _.Reffrer.Reffrer.Reffrer.Reffrer)
                                .ThenInclude(_ => _.Reffrer.Reffrer.Reffrer.Reffrer.Reffrer)
                                .FirstOrDefault();

                //get current user's immediate referer
                var firstUpline = findReferer.Reffrer;
                if (firstUpline != null)
                {
                    var firstReferrersFund = _context.SharerFunds.Where(_ => _.SharerId == firstUpline.Id).FirstOrDefault();
                    //if refferer has any fund


                    if (firstReferrersFund != null)
                    {
                        firstReferrersFund.MainAccount = firstReferrersFund.MainAccount + UplineAmount;
                        _context.Update(firstReferrersFund);
                        await _context.SaveChangesAsync();
                    }

                    //now for second referrer
                    var secondUpline = firstUpline.Reffrer;

                    if (secondUpline != null)
                    {
                        var secondUplineAccount = _context.SharerFunds.Where(_ => _.SharerId == secondUpline.Id).FirstOrDefault();



                        if (secondUplineAccount != null)
                        {
                            secondUplineAccount.MainAccount = secondUplineAccount.MainAccount + UplineAmount;
                            _context.Update(secondUplineAccount);
                            _context.SaveChanges();
                        }




                        var thirdUpline = firstUpline.Reffrer.Reffrer;

                        if (thirdUpline != null)
                        {
                            var thirdUplineAccount = _context.SharerFunds.Where(_ => _.SharerId == thirdUpline.Id).FirstOrDefault();



                            if (thirdUplineAccount != null)
                            {
                                thirdUplineAccount.MainAccount = thirdUplineAccount.MainAccount + UplineAmount;
                                _context.Update(thirdUplineAccount);
                                _context.SaveChanges();
                            }



                            var fourthUpline = firstUpline.Reffrer.Reffrer.Reffrer;

                            if (fourthUpline != null)
                            {
                                var fourthUplineAccount = _context.SharerFunds.Where(_ => _.SharerId == fourthUpline.Id).FirstOrDefault();



                                if (fourthUplineAccount != null)
                                {
                                    fourthUplineAccount.MainAccount = fourthUplineAccount.MainAccount + UplineAmount;
                                    _context.Update(fourthUplineAccount);
                                    _context.SaveChanges();
                                }


                                var fifthUpline = firstUpline.Reffrer.Reffrer.Reffrer.Reffrer;

                                if (fifthUpline != null)
                                {
                                    var fifthUplineAccount = _context.SharerFunds.Where(_ => _.SharerId == fifthUpline.Id).FirstOrDefault();



                                    if (fifthUplineAccount != null)
                                    {
                                        fifthUplineAccount.MainAccount = fifthUplineAccount.MainAccount + UplineAmount;
                                        _context.Update(fifthUplineAccount);
                                        _context.SaveChanges();
                                    }
                                }
                            }
                        }
                    }
                }


                var sharerPaymentListBeforeThisPayment = _context.Payments
                                   .Where(_ => _.PayerId == currentUserId)
                                   .Where(_ => _.PaymentType.PaymentTypeName == "MainAccountPromotional" || _.PaymentType.PaymentTypeName == "MainAccount")
                                   .Where(_ => _.ProductType.Name == "Promotional")
                                   .ToList();

                double sharerPaymentCountBeforeThisPayment = sharerPaymentListBeforeThisPayment.Select(_ => _.Amount).Sum();

                await _context.AddAsync(pay);

                await _context.SaveChangesAsync();

                var sharerPaymentList = _context.Payments
                                        .Where(_ => _.PayerId == currentUserId)
                                        .Where(_ => _.PaymentType.PaymentTypeName == "MainAccountPromotional" || _.PaymentType.PaymentTypeName == "MainAccount")
                                        .Where(_ => _.ProductType.Name == "Promotional")
                                        .ToList();

                double sharerPaymentCount = sharerPaymentList.Select(_ => _.Amount).Sum();

                var check = (TotalAmount + sharerPaymentCountBeforeThisPayment);

                var userAccount = _context.SharerFunds.Where(_ => _.SharerId == currentUserId).FirstOrDefault();

                if (sharerPaymentList.Count == 1 && TotalAmount == sharerPaymentCount && sharerPaymentCount > 1000)
                {



                    if (userAccount != null)
                    {
                        userAccount.BackShoppingAccount = (userAccount.BackShoppingAccount + ((25.0 * (sharerPaymentCount - 1000) / 100.0)));

                        _context.Update(userAccount);
                        _context.SaveChanges();
                    }
                }


                if (sharerPaymentList.Count > 1 && sharerPaymentCountBeforeThisPayment <= 1000)
                {
                    if (check > 1000)
                    {

                        if (userAccount != null)
                        {
                            userAccount.SharerId = currentUserId;
                            userAccount.BackShoppingAccount = (userAccount.BackShoppingAccount + ((25.0 * (check - 1000) / 100.0)));

                            _context.Update(userAccount);
                            _context.SaveChanges();
                        }
                    }

                }

                if (sharerPaymentCountBeforeThisPayment > 1000)
                {

                    if (userAccount != null)
                    {
                        userAccount.SharerId = currentUserId;
                        userAccount.BackShoppingAccount = (userAccount.BackShoppingAccount + ((25.0 * TotalAmount) / 100.0));

                        _context.Update(userAccount);
                        _context.SaveChanges();
                    }
                }

                var karrotAccount = await _context.KarrotFunds.Where(_ => _.KarrotCode == 21873465).FirstOrDefaultAsync();

                if (karrotAccount != null)
                {
                    karrotAccount.MainAccount = karrotAccount.MainAccount + karrotAmount;
                    karrotAccount.TotalIncome = karrotAccount.TotalIncome + karrotAmount;
                    _context.Update(karrotAccount);
                    await _context.SaveChangesAsync();

                }

                var ceoAccount = await _context.CeoFunds.Where(_ => _.CeoCode == 98327489).FirstOrDefaultAsync();

                if (ceoAccount != null)
                {
                    ceoAccount.MainAccount = ceoAccount.MainAccount + ceoAmount;
                    ceoAccount.TotalIncome = ceoAccount.TotalIncome + ceoAmount;
                    _context.Update(ceoAccount);
                    await _context.SaveChangesAsync();

                }

                return (currentUserDetails, role);
            }
            return Ok();
        }

        [HttpPost]
        public async Task<object> PaymentSubmitBackShoppingOffer([FromBody] Payment payment)
        {
            var currentUser = payment.PayerId;

            var currentUserDetails = _userManager.Users.Where(_ => _.Id == currentUser).FirstOrDefault();

            var role = await _context.Roles.Where(_ => _.Id == currentUserDetails.ApplicationRoleId).FirstOrDefaultAsync();

            var productType = _context.ProductTypes.Where(_ => _.Name == "Offer").FirstOrDefault();

            var fundExist = _context.SharerFunds.Where(_ => _.SharerId == currentUser).FirstOrDefault();

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
                    PaymentTime = DateTime.Now,
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

                    if (agentFundExist == null)
                    {
                        var fund = new AgentFund
                        {
                            MainAccount = amount,
                            TotalTransection = amount,
                            AgentName = findAgent.FirstName + findAgent.LastName

                        };
                        _context.Add(fund);
                        _context.SaveChanges();
                    }

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

                return (currentUserDetails, role);
            }
            return Ok();

        }

        [HttpPost]
        public async Task<object> PaymentSubmitBackShoppingPromotional([FromBody] Payment payment)
        {
            var currentUser = payment.PayerId;

            var currentUserDetails = _userManager.Users.Where(_ => _.Id == currentUser).FirstOrDefault();

            var role = await _context.Roles.Where(_ => _.Id == currentUserDetails.ApplicationRoleId).FirstOrDefaultAsync();

            var productType = _context.ProductTypes.Where(_ => _.Name == "Promotional").FirstOrDefault();

            var fundExist = _context.SharerFunds.Where(_ => _.SharerId == currentUser).FirstOrDefault();

            var paymentType = _context.PaymentTypes.Where(_ => _.PaymentTypeName == "BackShoppingPromotional").FirstOrDefault();

            if (fundExist.BackShoppingAccount < payment.Amount)
            {
                return Ok("You have No Money at Backshopping Account");
            }

            if (fundExist.BackShoppingAccount >= payment.Amount)
            {
                var pay = new Payment
                {
                    Amount = payment.Amount,
                    PayerId = currentUser,
                    PaymentTime = DateTime.Now,
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

                    if (agentFundExist == null)
                    {
                        var fund = new AgentFund
                        {
                            MainAccount = amount,
                            TotalTransection = amount,
                            AgentName = findAgent.FirstName + findAgent.LastName

                        };
                        _context.Add(fund);
                        _context.SaveChanges();
                    }

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

                _context.Add(pay);
                _context.SaveChanges();

                return (currentUserDetails, role);
            }
            return Ok();

        }

        [HttpPost]
        public async Task<object> WithdrawMoneyAppSharerToAgent([FromBody] WithdrawMoneyAppSharerToAgentResource withdraw)
        {
            var currentUser = withdraw.CurrentUserId;

            var currentUserDetails = _userManager.Users.Where(_ => _.Id == currentUser).FirstOrDefault();

            var role = await _context.Roles.Where(_ => _.Id == currentUserDetails.ApplicationRoleId).FirstOrDefaultAsync();

            var fundExist = _context.SharerFunds.Where(_ => _.SharerId == currentUser).FirstOrDefault();


            if (fundExist.MainAccount < withdraw.Amount)
            {
                return Ok("You have Not enough Money For CashOut");
            }

            if (fundExist.MainAccount >= withdraw.Amount)
            {
                var withdrawMoney = new WithdrawMoney
                {
                    Amount = withdraw.Amount,
                    UserId = currentUser,
                    PaymentTime = DateTime.Now,
                    AgentPhnNumber = withdraw.AgentPhnNumber,
                    UserName = currentUserDetails.FirstName,
                    UserPhoneNumber = currentUserDetails.PhoneNumber,

                };

                double amount = withdraw.Amount;

                var findAgent = _userManager.Users
                                .Where(_ => _.PhoneNumber == withdraw.AgentPhnNumber)
                                .FirstOrDefault();

                if (findAgent != null)
                {
                    var agentFundExist = _context.AgentFunds.Where(_ => _.AgentId == findAgent.Id).FirstOrDefault();

                    if (agentFundExist == null)
                    {
                        var fund = new AgentFund
                        {
                            AgentId = findAgent.Id,
                            MainAccount = amount,
                            TotalTransection = amount,
                            AgentName = findAgent.FirstName + findAgent.LastName,
                            AgentPhoneNumber = findAgent.PhoneNumber

                        };
                        _context.Add(fund);
                        _context.SaveChanges();
                    }

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
                    return (currentUserDetails, role, fundExist);
                }

                fundExist.MainAccount = fundExist.MainAccount - amount;
                _context.Update(fundExist);
                _context.SaveChanges();

                await _context.AddAsync(withdrawMoney);
                await _context.SaveChangesAsync();

                return (currentUserDetails, role);
            }


            return Ok();
        }

        [HttpPost]
        public async Task<object> WithdrawMoneyAppSharerToOfarz([FromBody] WithdrawMoneyAppSharerToOfarzResource withdraw)
        {
            var currentUser = withdraw.CurrentUserId;

            var currentUserDetails = _userManager.Users.Where(_ => _.Id == currentUser).FirstOrDefault();
            var role = _context.Roles.Where(_ => _.Id == currentUserDetails.ApplicationRoleId).FirstOrDefault();

            var fundExist = _context.SharerFunds.Where(_ => _.SharerId == currentUser).FirstOrDefault();


            if (fundExist.MainAccount < withdraw.Amount)
            {
                return Ok("You have Not enough Money For CashOut");
            }

            if (fundExist.MainAccount >= withdraw.Amount)
            {
                var withdrawMoney = new WithdrawMoney
                {
                    Amount = withdraw.Amount,
                    UserId = currentUser,
                    PaymentTime = DateTime.Now,
                    OfarzPhoneNumber = withdraw.OfarzPhnNumber,
                    UserName = currentUserDetails.FirstName,
                    UserPhoneNumber = currentUserDetails.PhoneNumber,

                };

                double amount = withdraw.Amount;

                var findOfarz = _userManager.Users
                                .Where(_ => _.PhoneNumber == withdraw.OfarzPhnNumber)
                                .FirstOrDefault();

                if (findOfarz != null)
                {
                    var ofarzFundExist = _context.OfarzFunds.Where(_ => _.OfarzId == findOfarz.Id).FirstOrDefault();

                    if (ofarzFundExist != null)
                    {

                        ofarzFundExist.MainAccount = ofarzFundExist.MainAccount + amount;

                        ofarzFundExist.GetMoneyByAppSharer = ofarzFundExist.GetMoneyByAppSharer + amount;

                        _context.Update(ofarzFundExist);
                        _context.SaveChanges();

                    }
                }
                if (findOfarz == null)
                {
                    return (currentUserDetails, role, fundExist);
                }

                fundExist.MainAccount = fundExist.MainAccount - amount;
                _context.Update(fundExist);
                _context.SaveChanges();

                await _context.AddAsync(withdrawMoney);
                await _context.SaveChangesAsync();

                return (currentUserDetails, role);
            }


            return Ok();
        }

        private object GenerateJwtToken(string mobilenNmber, IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, mobilenNmber),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
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
                string revUrl = Reverses.reverses(path_to_Images);
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
                string finalString = Reverses.reverses(sub);

                string f = finalString.Replace("\\", "/");
                return f;

            }


        }
    }

    public static class Reverses
    {
        public static string reverses(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

    }
}
