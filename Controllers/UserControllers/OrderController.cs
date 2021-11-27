using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ofarz_rest_api.Domain.Models.Account;
using ofarz_rest_api.Domain.Models.User;
using ofarz_rest_api.Persistence.Context;
using ofarz_rest_api.Resources.UserResources;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ofarz_rest_api.Controllers.UserControllers
{
    [Route("api/[controller]/[action]")]
    public class OrderController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        private readonly AppDbContext _context;

        public OrderController(SignInManager<ApplicationUser> signInManager,
                               UserManager<ApplicationUser> userManager,
                               RoleManager<ApplicationRole> roleManager,
                               AppDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }


        [HttpPost]
        public async Task<object> Checkout([FromBody]CheckOutResource checkOut)
        {
            var CurrentUserId = checkOut.CurrentUserId;

            var currentUserDetails = _userManager.Users.Where(_ => _.Id == CurrentUserId).FirstOrDefault();

            List<Product> products = checkOut.Products;

            Order corder = new Order();
            AgentOrder cagentOrder = new AgentOrder();

            corder.RoleId = currentUserDetails.ApplicationRoleId;
            cagentOrder.RoleId = currentUserDetails.ApplicationRoleId;

            corder.AddressId = 1;
            cagentOrder.AddressId = 1;

            corder.UserId = CurrentUserId;
            cagentOrder.UserId = CurrentUserId;

            corder.TotalAmount = checkOut.Amount;
            cagentOrder.TotalAmount = checkOut.Amount;

            _context.Add(corder);
            _context.SaveChanges();

            _context.Add(cagentOrder);
            _context.SaveChanges();


            var order = _context.Orders.Where(_ => _.Id == corder.Id).FirstOrDefault();
            var agentOrder = _context.AgentOrders.Where(_ => _.Id == cagentOrder.Id).FirstOrDefault();

            if (products != null)
            {


                foreach (var product in products)
                {
                    OrderDetail orderDetails = new OrderDetail();
                    orderDetails.OrderId = order.Id;
                    orderDetails.AgentOrderId = agentOrder.Id;
                    orderDetails.ProductId = product.Id;

                    _context.Add(orderDetails);
                    await _context.SaveChangesAsync();


                    AgentOrderDetails agentOrderDetails = new AgentOrderDetails();
                    agentOrderDetails.OrderId = order.Id;
                    agentOrderDetails.AgentOrderId = agentOrder.Id;
                    agentOrderDetails.ProductId = product.Id;

                    _context.Add(agentOrderDetails);
                    await _context.SaveChangesAsync();
                }

            }

            var rowCount = _context.Orders.Count() + 1;

            order.OrderNo = rowCount;
            agentOrder.OrderNo = rowCount;
            //order.OrderDate =

            order.UserName = currentUserDetails.FirstName;
            agentOrder.UserName = currentUserDetails.FirstName;

            order.PhoneNo = currentUserDetails.PhoneNumber;
            agentOrder.PhoneNo = currentUserDetails.PhoneNumber;

            var address = await _context.Markets
                          .Where(_ => _.Id == currentUserDetails.MarketId)
                          .Include(_ => _.UnionOrWard)
                          .ThenInclude(_ => _.Upozila)
                          .ThenInclude(_ => _.District)
                          .ThenInclude(_ => _.Division)
                          .FirstOrDefaultAsync();


            var agentFund = _context.AgentFunds.Where(_ => _.AgentId == CurrentUserId).FirstOrDefault();

            var ofarzFund = _context.OfarzFunds.Where(_ => _.MobileNumber == "22").FirstOrDefault();

            if (agentFund != null)
            {
                agentFund.MainAccount = agentFund.MainAccount - checkOut.Amount;
                _context.Update(agentFund);
                _context.SaveChanges();
            }

            if (ofarzFund != null)
            {
                ofarzFund.MainAccount = ofarzFund.MainAccount + checkOut.Amount;
                ofarzFund.GetMoneyByAgentShopping = ofarzFund.GetMoneyByAgentShopping + checkOut.Amount;
                _context.Update(ofarzFund);
                _context.SaveChanges();
            }


            _context.Update(order);
            await _context.SaveChangesAsync();

            _context.Update(agentOrder);
            await _context.SaveChangesAsync();

            return (order, agentOrder);
        }


        [HttpGet]
        public async Task<object> OrderList()
        {
            var ordrLst = await _context.Orders.ToListAsync();

            return ordrLst;
        }


        [HttpGet("{id}")]
        public async Task<object> OrderListAgent([FromRoute] string id)
        {
            var ordrLst = await _context.Orders
                                .Where(_ => _.User.Id == id)
                                .ToListAsync();

            return ordrLst;
        }



        [HttpGet("{id}")]
        public async Task<object> OrderDetails([FromRoute] int id)
        {

            var products = await _context
                            .OrderDetails
                            .Where(_ => _.OrderId == id)
                            .Select(p => p.Product)
                            .Include(_ => _.ProductType)
                            .Include(_ => _.Category)
                            .Include(_ => _.SubCategory)
                            .ToListAsync();

            return products;
        }


    }
}
