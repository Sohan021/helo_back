using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ofarz_rest_api.Persistence.Context;
using ofarz_rest_api.Resources.FundResources;
using System.Linq;
using System.Threading.Tasks;

namespace ofarz_rest_api.Controllers.FundController
{
    [Route("api/[controller]/[action]")]
    public class FundsController : Controller
    {
        private readonly AppDbContext _context;

        public FundsController(AppDbContext context)
        {
            _context = context;
        }

        #region Fund

        [HttpGet]
        public async Task<IActionResult> GetAgentFundList()
        {
            var agentFunds = await _context.AgentFunds.ToListAsync();

            return Ok(agentFunds);
        }

        [HttpGet]
        public async Task<IActionResult> GetAppSharerFundList()
        {
            var appSharerFund = await _context.SharerFunds.ToListAsync();

            return Ok(appSharerFund);
        }

        [HttpGet]
        public async Task<IActionResult> GetShoperFundList()
        {
            var appSharerFund = await _context.ShoperFunds.ToListAsync();

            return Ok(appSharerFund);
        }

        [HttpGet]
        public async Task<IActionResult> GetKarrotFund()
        {
            var appSharerFund = await _context.SharerFunds.ToListAsync();

            return Ok(appSharerFund);
        }


        [HttpGet]
        public async Task<IActionResult> GetCeoFund()
        {
            var appSharerFund = await _context.SharerFunds.ToListAsync();

            return Ok(appSharerFund);
        }



        #region Agent
        [HttpGet("{agentPhoneNumber}")]
        public async Task<IActionResult> GetAgentFund([FromRoute] AgentFundResource resource)
        {
            var agentFund = await _context
                                .AgentFunds
                                .Where(_ => _.AgentPhoneNumber == resource.AgentPhoneNumber)
                                .FirstOrDefaultAsync();

            return Ok(agentFund);
        }
        #endregion


        #region AppSharer
        [HttpGet("{appSharerPhoneNumber}")]
        public async Task<IActionResult> GetAppSharerFund([FromRoute]AppSharerFundResource resource)
        {
            var sharerFunds = await _context
                                .SharerFunds
                                .Where(_ => _.Sharer.PhoneNumber == resource.AppSharerPhoneNumber)
                                .FirstOrDefaultAsync();

            return Ok(sharerFunds);
        }
        #endregion


        #region Shoper
        [HttpGet("{shoperPhoneNumber}")]
        public async Task<IActionResult> GetShoperFund([FromRoute] ShoperFundResource resource)
        {
            var shoperFunds = await _context
                                .ShoperFunds
                                .Where(_ => _.Shoper.PhoneNumber == resource.ShoperPhoneNumber)
                                .FirstOrDefaultAsync();

            return Ok(shoperFunds);
        }
        #endregion


        #region Karrot

        [HttpGet]
        public async Task<IActionResult> GetKarrotFunds()
        {
            var karrotFunds = await _context
                                .KarrotFunds
                                .ToListAsync();

            return Ok(karrotFunds);
        }

        [HttpGet("{karrotPhoneNumber}")]
        public async Task<IActionResult> GetKarrotFund([FromRoute] KarrotFundResource resource)
        {
            var karrotFunds = await _context
                                .KarrotFunds
                                .Where(_ => _.Karrot.PhoneNumber == resource.KarrotPhoneNumber)
                                .FirstOrDefaultAsync();

            return Ok(karrotFunds);
        }
        #endregion


        #region Ceo
        [HttpGet]
        public async Task<IActionResult> GetCeoFunds()
        {
            var ceoFunds = await _context
                                .CeoFunds
                                .ToListAsync();

            return Ok(ceoFunds);
        }
        [HttpGet("{ceoPhoneNumber}")]
        public async Task<IActionResult> GetCeoFund([FromRoute] CeoFundResource resource)
        {
            var ceoFunds = await _context
                                .CeoFunds
                                .Where(_ => _.Ceo.PhoneNumber == resource.CeoPhoneNumber)
                                .FirstOrDefaultAsync();

            return Ok(ceoFunds);
        }
        #endregion

        #region Ofarz
        [HttpGet]
        public async Task<IActionResult> GetOfarzFunds()
        {
            var ceoFunds = await _context
                                .OfarzFunds

                                .ToListAsync();
            return Ok(ceoFunds);

        }
        [HttpGet("{ofarzPhonNumber}")]
        public async Task<IActionResult> GetOfarzFund([FromRoute] OfarzFundResource resource)
        {
            var ceoFunds = await _context
                                .OfarzFunds
                                .Where(_ => _.MobileNumber == resource.OfarzPhonNumber)
                                .FirstOrDefaultAsync();

            return Ok(ceoFunds);
        }
        #endregion


        #endregion



        #region Withdraw
        [HttpGet]
        public async Task<IActionResult> GetAllWithdrawList()
        {
            var ceoFunds = await _context
                                .WithdrawMoney
                                .ToListAsync();

            return Ok(ceoFunds);
        }




        [HttpGet("{appSharerPhoneNumber}")]
        public async Task<IActionResult> GetAppSharerWithdrawListToAgent([FromRoute] AppSharerWithdrawResource resource)
        {
            var ceoFunds = await _context
                                .WithdrawMoney
                                .Where(_ => _.UserPhoneNumber == resource.AppSharerPhoneNumber)
                                .Where(_ => _.OfarzPhoneNumber == null)
                                .ToListAsync();

            return Ok(ceoFunds);
        }



        [HttpGet("{appSharerPhoneNumber}")]
        public async Task<IActionResult> GetAppSharerWithdrawListToOfarz([FromRoute] AppSharerWithdrawResource resource)
        {
            var ceoFunds = await _context
                                .WithdrawMoney
                                .Where(_ => _.UserPhoneNumber == resource.AppSharerPhoneNumber)
                                .Where(_ => _.AgentPhnNumber == null)
                                .ToListAsync();

            return Ok(ceoFunds);
        }


        [HttpGet("{agentPhoneNumber}")]
        public async Task<IActionResult> GetAgentWithdrawListToOfarz([FromRoute] AgentWithdrawResource resource)
        {
            var ceoFunds = await _context
                                .WithdrawMoney
                                .Where(_ => _.UserPhoneNumber == resource.AgentPhoneNumber)
                                .Where(_ => _.AgentPhnNumber == null)
                                .ToListAsync();

            return Ok(ceoFunds);
        }

        [HttpGet("{agentPhoneNumber}")]
        public async Task<IActionResult> GetAgentWithdrawListFromUser([FromRoute] AgentWithdrawResource resource)
        {
            var ceoFunds = await _context
                                .WithdrawMoney
                                .Where(_ => _.AgentPhnNumber == resource.AgentPhoneNumber)
                                .Include(_ => _.User).ThenInclude(_ => _.ApplicationRole)
                                .ToListAsync();

            return Ok(ceoFunds);
        }


        [HttpGet("{karrotPhoneNumber}")]
        public async Task<IActionResult> GetKarrotWithdrawListToAgent([FromRoute] KarrotWithdrawResource resource)
        {
            var ceoFunds = await _context
                                .WithdrawMoney
                                .Where(_ => _.UserPhoneNumber == resource.KarrotPhoneNumber)
                                .Where(_ => _.OfarzPhoneNumber == null)
                                .ToListAsync();

            return Ok(ceoFunds);
        }


        [HttpGet("{karrotPhoneNumber}")]
        public async Task<IActionResult> GetKarrotWithdrawListToOfarz([FromRoute] KarrotWithdrawResource resource)
        {
            var ceoFunds = await _context
                                .WithdrawMoney
                                .Where(_ => _.UserPhoneNumber == resource.KarrotPhoneNumber)
                                .Where(_ => _.AgentPhnNumber == null)
                                .ToListAsync();

            return Ok(ceoFunds);
        }


        [HttpGet("{ceoPhoneNumber}")]
        public async Task<IActionResult> GetCeoWithdrawListToAgent([FromRoute] CeoWithdrawResource resource)
        {
            var ceoFunds = await _context
                                .WithdrawMoney
                                .Where(_ => _.UserPhoneNumber == resource.CeoPhoneNumber)
                                .Where(_ => _.OfarzPhoneNumber == null)
                                .ToListAsync();

            return Ok(ceoFunds);
        }


        [HttpGet("{ceoPhoneNumber}")]
        public async Task<IActionResult> GetCeoWithdrawListToOfarz([FromRoute] CeoWithdrawResource resource)
        {
            var ceoFunds = await _context
                                .WithdrawMoney
                                .Where(_ => _.UserPhoneNumber == resource.CeoPhoneNumber)
                                .Where(_ => _.AgentPhnNumber == null)
                                .ToListAsync();

            return Ok(ceoFunds);
        }









        [HttpGet("{ofarzPhoneNumber}")]
        public async Task<IActionResult> GetOfarzWithdrawListFromAppSharer([FromRoute] OfarzWithdrawResource resource)
        {
            var ceoFunds = await _context
                                .WithdrawMoney
                                .Where(_ => _.OfarzPhoneNumber == resource.OfarzPhoneNumber)
                                .Where(_ => _.User.ApplicationRole.Name == "AppSharer")
                                .ToListAsync();

            return Ok(ceoFunds);
        }

        [HttpGet("{ofarzPhoneNumber}")]
        public async Task<IActionResult> GetOfarzWithdrawListFromKarrot([FromRoute] OfarzWithdrawResource resource)
        {
            var ceoFunds = await _context
                                .WithdrawMoney
                                .Where(_ => _.OfarzPhoneNumber == resource.OfarzPhoneNumber)
                                .Where(_ => _.User.ApplicationRole.Name == "Karrot")
                                .ToListAsync();

            return Ok(ceoFunds);
        }

        [HttpGet("{ofarzPhoneNumber}")]
        public async Task<IActionResult> GetOfarzWithdrawListFromCeo([FromRoute] OfarzWithdrawResource resource)
        {
            var ceoFunds = await _context
                                .WithdrawMoney
                                .Where(_ => _.OfarzPhoneNumber == resource.OfarzPhoneNumber)
                                .Where(_ => _.User.ApplicationRole.Name == "CEO")
                                .ToListAsync();

            return Ok(ceoFunds);
        }

        #endregion
    }
}
