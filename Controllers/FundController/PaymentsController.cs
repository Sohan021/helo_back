using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ofarz_rest_api.Persistence.Context;
using ofarz_rest_api.Resources.FundResources;
using System.Linq;
using System.Threading.Tasks;

namespace ofarz_rest_api.Controllers.FundController
{
    [Route("api/[controller]/[action]")]
    public class PaymentsController : Controller
    {

        private readonly AppDbContext _context;

        public PaymentsController(AppDbContext context)
        {
            _context = context;
        }

        #region PaymentList
        [HttpGet]
        public async Task<IActionResult> GetAllPaymentList()
        {
            var paymentList = await _context
                                    .Payments
                                    .Include(_ => _.PaymentType)
                                    .Include(_ => _.ProductType)
                                    .Include(_ => _.Payer)
                                    .ToListAsync();

            return Ok(paymentList);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllPaymentListManual([FromBody] int productTypeId,
                                                                            int paymentTypeId,
                                                                            string agentPhoneNumber,
                                                                            string appSharerPhoneNumber)
        {
            #region If One variable null
            if (productTypeId == 0 && paymentTypeId != 0 && agentPhoneNumber != null && appSharerPhoneNumber != null)
            {
                var paymentList = await _context.Payments
                                        .Where(_ => _.PaymentType.Id == paymentTypeId)
                                        .Where(_ => _.AgentPhnNumber == agentPhoneNumber)
                                        .Where(_ => _.Payer.PhoneNumber == appSharerPhoneNumber)
                                        .Include(_ => _.PaymentType)
                                        .Include(_ => _.ProductType)
                                        .Include(_ => _.Payer)
                                        .ToListAsync();

                return Ok(paymentList);
            }

            if (productTypeId != 0 && paymentTypeId == 0 && agentPhoneNumber != null && appSharerPhoneNumber != null)
            {
                var paymentList = await _context.Payments
                                        .Where(_ => _.ProductType.Id == productTypeId)
                                        .Where(_ => _.AgentPhnNumber == agentPhoneNumber)
                                        .Where(_ => _.Payer.PhoneNumber == appSharerPhoneNumber)
                                        .Include(_ => _.PaymentType)
                                        .Include(_ => _.ProductType)
                                        .Include(_ => _.Payer)
                                        .ToListAsync();

                return Ok(paymentList);
            }


            if (productTypeId != 0 && paymentTypeId != 0 && agentPhoneNumber == null && appSharerPhoneNumber != null)
            {
                var paymentList = await _context.Payments
                                        .Where(_ => _.PaymentType.Id == paymentTypeId)
                                        .Where(_ => _.ProductType.Id == productTypeId)
                                        .Where(_ => _.Payer.PhoneNumber == appSharerPhoneNumber)
                                        .Include(_ => _.PaymentType)
                                        .Include(_ => _.ProductType)
                                        .Include(_ => _.Payer)
                                        .ToListAsync();

                return Ok(paymentList);
            }

            if (productTypeId != 0 && paymentTypeId != 0 && agentPhoneNumber != null && appSharerPhoneNumber == null)
            {
                var paymentList = await _context.Payments
                                        .Where(_ => _.PaymentType.Id == paymentTypeId)
                                        .Where(_ => _.ProductType.Id == productTypeId)
                                        .Where(_ => _.AgentPhnNumber == agentPhoneNumber)
                                        .Include(_ => _.PaymentType)
                                        .Include(_ => _.ProductType)
                                        .Include(_ => _.Payer)
                                        .ToListAsync();

                return Ok(paymentList);
            }
            #endregion

            #region If Two variable null

            if (productTypeId == 0 && paymentTypeId == 0 && agentPhoneNumber != null && appSharerPhoneNumber != null)
            {
                var paymentList = await _context.Payments
                                        .Where(_ => _.AgentPhnNumber == agentPhoneNumber)
                                        .Where(_ => _.Payer.PhoneNumber == appSharerPhoneNumber)
                                        .Include(_ => _.PaymentType)
                                        .Include(_ => _.ProductType)
                                        .Include(_ => _.Payer)
                                        .ToListAsync();

                return Ok(paymentList);
            }

            if (productTypeId == 0 && paymentTypeId != 0 && agentPhoneNumber == null && appSharerPhoneNumber != null)
            {
                var paymentList = await _context.Payments
                                        .Where(_ => _.PaymentType.Id == paymentTypeId)
                                        .Where(_ => _.Payer.PhoneNumber == appSharerPhoneNumber)
                                        .Include(_ => _.PaymentType)
                                        .Include(_ => _.ProductType)
                                        .Include(_ => _.Payer)
                                        .ToListAsync();

                return Ok(paymentList);
            }

            if (productTypeId == 0 && paymentTypeId != 0 && agentPhoneNumber != null && appSharerPhoneNumber == null)
            {
                var paymentList = await _context.Payments
                                        .Where(_ => _.PaymentType.Id == paymentTypeId)
                                        .Where(_ => _.AgentPhnNumber == agentPhoneNumber)
                                        .Include(_ => _.PaymentType)
                                        .Include(_ => _.ProductType)
                                        .Include(_ => _.Payer)
                                        .ToListAsync();

                return Ok(paymentList);
            }

            if (productTypeId != 0 && paymentTypeId == 0 && agentPhoneNumber == null && appSharerPhoneNumber != null)
            {
                var paymentList = await _context.Payments
                                        .Where(_ => _.ProductType.Id == productTypeId)
                                        .Where(_ => _.Payer.Id == appSharerPhoneNumber)
                                        .Include(_ => _.PaymentType)
                                        .Include(_ => _.ProductType)
                                        .Include(_ => _.Payer)
                                        .ToListAsync();

                return Ok(paymentList);
            }

            if (productTypeId != 0 && paymentTypeId == 0 && agentPhoneNumber != null && appSharerPhoneNumber == null)
            {
                var paymentList = await _context.Payments
                                        .Where(_ => _.ProductType.Id == productTypeId)
                                        .Where(_ => _.AgentPhnNumber == agentPhoneNumber)
                                        .Include(_ => _.PaymentType)
                                        .Include(_ => _.ProductType)
                                        .Include(_ => _.Payer)
                                        .ToListAsync();

                return Ok(paymentList);
            }

            if (productTypeId != 0 && paymentTypeId != 0 && agentPhoneNumber == null && appSharerPhoneNumber == null)
            {
                var paymentList = await _context.Payments
                                        .Where(_ => _.AgentPhnNumber == agentPhoneNumber)
                                        .Where(_ => _.Payer.Id == appSharerPhoneNumber)
                                        .Include(_ => _.PaymentType)
                                        .Include(_ => _.ProductType)
                                        .Include(_ => _.Payer)
                                        .ToListAsync();

                return Ok(paymentList);
            }

            #endregion

            #region If Three variable null

            if (productTypeId == 0 && paymentTypeId == 0 && agentPhoneNumber == null && appSharerPhoneNumber != null)
            {
                var paymentList = await _context.Payments
                                        .Where(_ => _.Payer.Id == appSharerPhoneNumber)
                                        .Include(_ => _.PaymentType)
                                        .Include(_ => _.ProductType)
                                        .Include(_ => _.Payer)
                                        .ToListAsync();

                return Ok(paymentList);
            }


            if (productTypeId == 0 && paymentTypeId == 0 && agentPhoneNumber != null && appSharerPhoneNumber == null)
            {
                var paymentList = await _context.Payments
                                        .Where(_ => _.AgentPhnNumber == agentPhoneNumber)
                                        .Include(_ => _.PaymentType)
                                        .Include(_ => _.ProductType)
                                        .Include(_ => _.Payer)
                                        .ToListAsync();

                return Ok(paymentList);
            }

            if (productTypeId == 0 && paymentTypeId != 0 && agentPhoneNumber == null && appSharerPhoneNumber == null)
            {
                var paymentList = await _context.Payments
                                        .Where(_ => _.PaymentType.Id == paymentTypeId)
                                        .Include(_ => _.PaymentType)
                                        .Include(_ => _.ProductType)
                                        .Include(_ => _.Payer)
                                        .ToListAsync();

                return Ok(paymentList);
            }


            if (productTypeId != 0 && paymentTypeId == 0 && agentPhoneNumber == null && appSharerPhoneNumber == null)
            {
                var paymentList = await _context.Payments
                                        .Where(_ => _.ProductType.Id == productTypeId)
                                        .Include(_ => _.PaymentType)
                                        .Include(_ => _.ProductType)
                                        .Include(_ => _.Payer)
                                        .ToListAsync();

                return Ok(paymentList);
            }

            #endregion

            #region If Four variable null
            if (productTypeId == 0 && paymentTypeId == 0 && agentPhoneNumber == null && appSharerPhoneNumber == null)
            {
                var paymentList = await _context
                                    .Payments
                                    .Include(_ => _.PaymentType)
                                    .Include(_ => _.ProductType)
                                    .Include(_ => _.Payer)
                                    .ToListAsync();

                return Ok(paymentList);
            }
            #endregion

            #region If All Variable ar here
            if (productTypeId != 0 && paymentTypeId != 0 && agentPhoneNumber != null && appSharerPhoneNumber != null)
            {
                var paymentList = await _context.Payments
                                        .Where(_ => _.ProductType.Id == productTypeId)
                                        .Where(_ => _.PaymentType.Id == paymentTypeId)
                                        .Where(_ => _.AgentPhnNumber == agentPhoneNumber)
                                        .Where(_ => _.Payer.PhoneNumber == appSharerPhoneNumber)
                                        .Include(_ => _.PaymentType)
                                        .Include(_ => _.ProductType)
                                        .Include(_ => _.Payer)
                                        .ToListAsync();

                return Ok(paymentList);
            }
            #endregion

            #region Else
            else
            {
                var paymentList = await _context.Payments
                                    .Include(_ => _.PaymentType)
                                    .Include(_ => _.ProductType)
                                    .Include(_ => _.Payer)
                                    .ToListAsync();

                return Ok(paymentList);
            }
            #endregion




        }


        [HttpGet("{agentPhoneNumber}")]
        public async Task<IActionResult> GetAllPaymentListAgent([FromRoute] AgentPaymentListResource resource)
        {

            var paymentList = await _context.Payments
                                    .Where(_ => _.AgentPhnNumber == resource.AgentPhoneNumber)
                                    .Include(_ => _.PaymentType)
                                    .Include(_ => _.ProductType)
                                    .Include(_ => _.Payer)
                                    .ThenInclude(_ => _.ApplicationRole)
                                    .ToListAsync();
            return Ok(paymentList);
        }

        [HttpGet("{appSharerPhoneNumber}")]
        public async Task<IActionResult> GetAllPaymentListAppSharer([FromRoute] AppSharerPaymentListResource resource)
        {
            var paymentList = await _context.Payments
                                    .Where(_ => _.Payer.PhoneNumber == resource.AppSharerPhoneNumber)
                                    .Include(_ => _.PaymentType)
                                    .Include(_ => _.ProductType)
                                    .Include(_ => _.Payer)
                                    .ToListAsync();
            return Ok(paymentList);
        }

        [HttpGet("{shoperPhoneNumber}")]
        public async Task<IActionResult> GetAllPaymentListShoper([FromRoute] ShoperPaymentListResource resource)
        {
            var paymentList = await _context.Payments
                                    .Where(_ => _.Payer.PhoneNumber == resource.ShoperPhoneNumber)
                                    .Include(_ => _.PaymentType)
                                    .Include(_ => _.ProductType)
                                    .Include(_ => _.Payer)
                                    .ToListAsync();
            return Ok(paymentList);
        }


        #endregion


        #region Payment List Table Cash Offer

        [HttpGet]
        public async Task<IActionResult> GetAllPaymentListTableCashOffer()
        {
            var productType = _context.ProductTypes.Where(_ => _.Name == "Offer").FirstOrDefault();

            var paymentType = _context.PaymentTypes.Where(_ => _.PaymentTypeName == "TableCashOffer").FirstOrDefault();

            var paymentList = await _context.Payments
                                    .Where(_ => _.PaymentTypeId == paymentType.Id)
                                    .Where(_ => _.ProductTypeId == productType.Id)
                                    .Include(_ => _.PaymentType)
                                    .Include(_ => _.ProductType)
                                    .Include(_ => _.Payer)
                                    .ToListAsync();
            return Ok(paymentList);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaymentListTableCashOfferAgent([FromRoute] AgentPaymentListResource resource)
        {
            var productType = _context.ProductTypes.Where(_ => _.Name == "Offer").FirstOrDefault();

            var paymentType = _context.PaymentTypes.Where(_ => _.PaymentTypeName == "TableCashOffer").FirstOrDefault();

            var paymentList = await _context.Payments
                                    .Where(_ => _.AgentPhnNumber == resource.AgentPhoneNumber)
                                    .Where(_ => _.PaymentTypeId == paymentType.Id)
                                    .Where(_ => _.ProductTypeId == productType.Id)
                                    .Include(_ => _.PaymentType)
                                    .Include(_ => _.ProductType)
                                    .Include(_ => _.Payer)
                                    .ToListAsync();
            return Ok(paymentList);
        }


        [HttpGet("{appSharerPhoneNumber}")]
        public async Task<IActionResult> GetAllPaymentListTableCashOfferAppSharer([FromRoute] AppSharerPaymentListResource resource)
        {
            var productType = _context.ProductTypes.Where(_ => _.Name == "Offer").FirstOrDefault();

            var paymentType = _context.PaymentTypes.Where(_ => _.PaymentTypeName == "TableCashOffer").FirstOrDefault();

            var paymentList = await _context.Payments
                                    .Where(_ => _.PayerPhoneNumber == resource.AppSharerPhoneNumber)
                                    .Where(_ => _.PaymentTypeId == paymentType.Id)
                                    .Where(_ => _.ProductTypeId == productType.Id)
                                    .Include(_ => _.PaymentType)
                                    .Include(_ => _.ProductType)
                                    .Include(_ => _.Payer)
                                    .ToListAsync();
            return Ok(paymentList);
        }

        [HttpGet("{shoperPhoneNumber}")]
        public async Task<IActionResult> GetAllPaymentListTableCashOfferShoper([FromRoute] ShoperPaymentListResource resource)
        {
            var productType = _context.ProductTypes.Where(_ => _.Name == "Offer").FirstOrDefault();

            var paymentType = _context.PaymentTypes.Where(_ => _.PaymentTypeName == "TableCashOffer").FirstOrDefault();

            var paymentList = await _context.Payments
                                    .Where(_ => _.PayerPhoneNumber == resource.ShoperPhoneNumber)
                                    .Where(_ => _.PaymentTypeId == paymentType.Id)
                                    .Where(_ => _.ProductTypeId == productType.Id)
                                    .Include(_ => _.PaymentType)
                                    .Include(_ => _.ProductType)
                                    .Include(_ => _.Payer)
                                    .ToListAsync();
            return Ok(paymentList);
        }


        #endregion Payment List Table Cash Promotional


        #region Payment List Table Cash Promotional

        [HttpGet]
        public async Task<IActionResult> GetAllPaymentListTableCashPromotional()
        {
            var productType = _context.ProductTypes.Where(_ => _.Name == "Promotional").FirstOrDefault();

            var paymentType = _context.PaymentTypes.Where(_ => _.PaymentTypeName == "TableCashPromotional").FirstOrDefault();

            var paymentList = await _context.Payments
                                     .Where(_ => _.ProductTypeId == productType.Id)
                                     .Where(_ => _.ProductTypeId == paymentType.Id)
                                     .Include(_ => _.PaymentType)
                                     .Include(_ => _.ProductType)
                                     .Include(_ => _.Payer)
                                     .ToListAsync();
            return Ok(paymentList);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaymentListTableCashPromotionalAgent([FromRoute] AgentPaymentListResource resource)
        {
            var productType = _context.ProductTypes.Where(_ => _.Name == "Promotional").FirstOrDefault();

            var paymentType = _context.PaymentTypes.Where(_ => _.PaymentTypeName == "TableCashPromotional").FirstOrDefault();

            var paymentList = await _context.Payments
                                    .Where(_ => _.ProductTypeId == productType.Id)
                                    .Where(_ => _.ProductTypeId == paymentType.Id)
                                    .Where(_ => _.AgentPhnNumber == resource.AgentPhoneNumber)
                                    .Include(_ => _.PaymentType)
                                    .Include(_ => _.ProductType)
                                    .Include(_ => _.Payer)
                                    .ToListAsync();
            return Ok(paymentList);
        }

        [HttpGet("{appSharerPhoneNumber}")]
        public async Task<IActionResult> GetAllPaymentListTableCashPromotionalAppSharer([FromRoute] AppSharerPaymentListResource resource)
        {
            var productType = _context.ProductTypes.Where(_ => _.Name == "Promotional").FirstOrDefault();

            var paymentType = _context.PaymentTypes.Where(_ => _.PaymentTypeName == "TableCashPromotional").FirstOrDefault();

            var paymentList = await _context.Payments
                                    .Where(_ => _.ProductTypeId == productType.Id)
                                    .Where(_ => _.ProductTypeId == paymentType.Id)
                                    .Where(_ => _.PayerPhoneNumber == resource.AppSharerPhoneNumber)
                                    .Include(_ => _.PaymentType)
                                    .Include(_ => _.ProductType)
                                    .Include(_ => _.Payer)
                                    .ToListAsync();
            return Ok(paymentList);
        }

        #endregion


        #region Payment List MainAccount Offer

        [HttpGet]
        public async Task<IActionResult> GetAllPaymentListMainAccountOffer()
        {
            var productType = _context.ProductTypes.Where(_ => _.Name == "Offer").FirstOrDefault();

            var paymentType = _context.PaymentTypes.Where(_ => _.PaymentTypeName == "MainAccount").FirstOrDefault();

            var paymentList = await _context.Payments
                                    .Where(_ => _.ProductTypeId == productType.Id)
                                    .Where(_ => _.ProductTypeId == paymentType.Id)
                                    .Include(_ => _.PaymentType)
                                    .Include(_ => _.ProductType)
                                    .Include(_ => _.Payer)
                                    .ToListAsync();
            return Ok(paymentList);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaymentListMainAccountOfferAgent([FromRoute] AgentPaymentListResource resource)
        {
            var productType = _context.ProductTypes.Where(_ => _.Name == "Offer").FirstOrDefault();

            var paymentType = _context.PaymentTypes.Where(_ => _.PaymentTypeName == "MainAccount").FirstOrDefault();

            var paymentList = await _context.Payments
                                    .Where(_ => _.ProductTypeId == productType.Id)
                                    .Where(_ => _.ProductTypeId == paymentType.Id)
                                    .Where(_ => _.AgentPhnNumber == resource.AgentPhoneNumber)
                                    .Include(_ => _.PaymentType)
                                    .Include(_ => _.ProductType)
                                    .Include(_ => _.Payer)
                                    .ToListAsync();
            return Ok(paymentList);
        }

        [HttpGet("{appSharerPhoneNumber}")]
        public async Task<IActionResult> GetAllPaymentListMainAccountOfferAppSharer([FromRoute] AppSharerPaymentListResource resource)
        {
            var productType = _context.ProductTypes.Where(_ => _.Name == "Offer").FirstOrDefault();

            var paymentType = _context.PaymentTypes.Where(_ => _.PaymentTypeName == "MainAccountOffer").FirstOrDefault();

            var paymentList = await _context.Payments
                                    .Where(_ => _.ProductTypeId == productType.Id)
                                    .Where(_ => _.ProductTypeId == paymentType.Id)
                                    .Where(_ => _.PayerPhoneNumber == resource.AppSharerPhoneNumber)
                                    .Include(_ => _.PaymentType)
                                    .Include(_ => _.ProductType)
                                    .Include(_ => _.Payer)
                                    .ToListAsync();
            return Ok(paymentList);
        }

        #endregion


        #region Payment List MainAccount Promotional

        [HttpGet]
        public async Task<IActionResult> GetAllPaymentListMainAccountPromotional()
        {
            var productType = _context.ProductTypes.Where(_ => _.Name == "Promotional").FirstOrDefault();

            var paymentType = _context.PaymentTypes.Where(_ => _.PaymentTypeName == "MainAccountPromotional").FirstOrDefault();

            var paymentList = await _context.Payments
                                    .Where(_ => _.ProductTypeId == productType.Id)
                                    .Where(_ => _.ProductTypeId == paymentType.Id)
                                    .Include(_ => _.PaymentType)
                                    .Include(_ => _.ProductType)
                                    .Include(_ => _.Payer)
                                    .ToListAsync();
            return Ok(paymentList);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaymentListMainAccountPromotionalAgent([FromRoute] AgentPaymentListResource resource)
        {
            var productType = _context.ProductTypes.Where(_ => _.Name == "Promotional").FirstOrDefault();

            var paymentType = _context.PaymentTypes.Where(_ => _.PaymentTypeName == "MainAccountPromotional").FirstOrDefault();

            var paymentList = await _context.Payments
                                    .Where(_ => _.ProductTypeId == productType.Id)
                                    .Where(_ => _.ProductTypeId == paymentType.Id)
                                    .Where(_ => _.AgentPhnNumber == resource.AgentPhoneNumber)
                                    .Include(_ => _.PaymentType)
                                    .Include(_ => _.ProductType)
                                    .Include(_ => _.Payer)
                                    .ToListAsync();
            return Ok(paymentList);
        }

        [HttpGet("{appSharerPhoneNumber}")]
        public async Task<IActionResult> GetAllPaymentListMainAccountPromotionalAppSharer([FromRoute] AppSharerPaymentListResource resource)
        {
            var productType = _context.ProductTypes.Where(_ => _.Name == "Promotional").FirstOrDefault();

            var paymentType = _context.PaymentTypes.Where(_ => _.PaymentTypeName == "MainAccountPromotional").FirstOrDefault();


            var paymentList = await _context.Payments
                                    .Where(_ => _.ProductTypeId == productType.Id)
                                    .Where(_ => _.ProductTypeId == paymentType.Id)
                                    .Where(_ => _.PayerPhoneNumber == resource.AppSharerPhoneNumber)
                                    .Include(_ => _.PaymentType)
                                    .Include(_ => _.ProductType)
                                    .Include(_ => _.Payer)
                                    .ToListAsync();
            return Ok(paymentList);
        }

        #endregion


        #region Payment List Backshoppimg Offer

        [HttpGet]
        public async Task<IActionResult> GetAllPaymentListBackShoppingOffer()
        {
            var productType = _context.ProductTypes.Where(_ => _.Name == "Offer").FirstOrDefault();

            var paymentType = _context.PaymentTypes.Where(_ => _.PaymentTypeName == "BackShoppingOffer").FirstOrDefault();

            var paymentList = await _context.Payments
                                    .Where(_ => _.ProductTypeId == productType.Id)
                                    .Where(_ => _.ProductTypeId == paymentType.Id)
                                    .Include(_ => _.PaymentType)
                                    .Include(_ => _.ProductType)
                                    .Include(_ => _.Payer)
                                    .ToListAsync();
            return Ok(paymentList);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaymentListBackShoppingOfferAgnet([FromRoute] AgentPaymentListResource resource)
        {
            var productType = _context.ProductTypes.Where(_ => _.Name == "Offer").FirstOrDefault();

            var paymentType = _context.PaymentTypes.Where(_ => _.PaymentTypeName == "BackShoppingOffer").FirstOrDefault();

            var paymentList = await _context.Payments
                                    .Where(_ => _.ProductTypeId == productType.Id)
                                    .Where(_ => _.ProductTypeId == paymentType.Id)
                                    .Where(_ => _.AgentPhnNumber == resource.AgentPhoneNumber)
                                    .Include(_ => _.PaymentType)
                                    .Include(_ => _.ProductType)
                                    .Include(_ => _.Payer)
                                    .ToListAsync();
            return Ok(paymentList);
        }

        [HttpGet("{appSharerPhoneNumber}")]
        public async Task<IActionResult> GetAllPaymentListBackShoppingOfferAppSharer([FromRoute] AppSharerPaymentListResource resource)
        {
            var productType = _context.ProductTypes.Where(_ => _.Name == "Offer").FirstOrDefault();

            var paymentType = _context.PaymentTypes.Where(_ => _.PaymentTypeName == "BackShoppingOffer").FirstOrDefault();

            var paymentList = await _context.Payments
                                    .Where(_ => _.ProductTypeId == productType.Id)
                                    .Where(_ => _.ProductTypeId == paymentType.Id)
                                    .Where(_ => _.PayerPhoneNumber == resource.AppSharerPhoneNumber)
                                    .Include(_ => _.PaymentType)
                                    .Include(_ => _.ProductType)
                                    .Include(_ => _.Payer)
                                    .ToListAsync();
            return Ok(paymentList);
        }

        [HttpGet("{shoperPhoneNumber}")]
        public async Task<IActionResult> GetAllPaymentListBackShoppingOfferShoper([FromRoute] ShoperPaymentListResource resource)
        {
            var productType = _context.ProductTypes.Where(_ => _.Name == "Offer").FirstOrDefault();

            var paymentType = _context.PaymentTypes.Where(_ => _.PaymentTypeName == "BackShoppingOffer").FirstOrDefault();

            var paymentList = await _context.Payments
                                    .Where(_ => _.ProductTypeId == productType.Id)
                                    .Where(_ => _.ProductTypeId == paymentType.Id)
                                    .Where(_ => _.PayerPhoneNumber == resource.ShoperPhoneNumber)
                                    .Include(_ => _.PaymentType)
                                    .Include(_ => _.ProductType)
                                    .Include(_ => _.Payer)
                                    .ToListAsync();
            return Ok(paymentList);
        }

        #endregion


        #region Payment List Backshoppimg Promotional

        [HttpGet]
        public async Task<IActionResult> GetAllPaymentListBackShoppingPromotional()
        {
            var productType = _context.ProductTypes.Where(_ => _.Name == "Promotional").FirstOrDefault();

            var paymentType = _context.PaymentTypes.Where(_ => _.PaymentTypeName == "BackShoppingPromotional").FirstOrDefault();

            var paymentList = await _context.Payments
                .Where(_ => _.ProductTypeId == productType.Id)
                                    .Where(_ => _.ProductTypeId == paymentType.Id)
                                    .Include(_ => _.PaymentType)
                                    .Include(_ => _.ProductType)
                                    .Include(_ => _.Payer)
                                    .ToListAsync();
            return Ok(paymentList);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaymentListBackShoppingPromotionalAgnet([FromRoute] AgentPaymentListResource resource)
        {
            var productType = _context.ProductTypes.Where(_ => _.Name == "Promotional").FirstOrDefault();

            var paymentType = _context.PaymentTypes.Where(_ => _.PaymentTypeName == "BackShoppingPromotional").FirstOrDefault();

            var paymentList = await _context.Payments
                .Where(_ => _.ProductTypeId == productType.Id)
                                    .Where(_ => _.ProductTypeId == paymentType.Id)
                                    .Where(_ => _.AgentPhnNumber == resource.AgentPhoneNumber)
                                    .Include(_ => _.PaymentType)
                                    .Include(_ => _.ProductType)
                                    .Include(_ => _.Payer)
                                    .ToListAsync();
            return Ok(paymentList);
        }

        [HttpGet("{appSharerPhoneNumber}")]
        public async Task<IActionResult> GetAllPaymentListBackShoppingPromotionalAppSharer([FromRoute] AppSharerPaymentListResource resource)
        {
            var productType = _context.ProductTypes.Where(_ => _.Name == "Promotional").FirstOrDefault();

            var paymentType = _context.PaymentTypes.Where(_ => _.PaymentTypeName == "BackShoppingPromotional").FirstOrDefault();

            var paymentList = await _context.Payments
                .Where(_ => _.ProductTypeId == productType.Id)
                                    .Where(_ => _.ProductTypeId == paymentType.Id)
                                    .Where(_ => _.PayerPhoneNumber == resource.AppSharerPhoneNumber)
                                    .Include(_ => _.PaymentType)
                                    .Include(_ => _.ProductType)
                                    .Include(_ => _.Payer)
                                    .ToListAsync();
            return Ok(paymentList);
        }

        #endregion




    }
}
