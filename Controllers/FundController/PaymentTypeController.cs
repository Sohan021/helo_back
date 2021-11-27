using Microsoft.AspNetCore.Mvc;
using ofarz_rest_api.Domain.IService.IFundServices;
using ofarz_rest_api.Domain.Models.Fund;
using ofarz_rest_api.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ofarz_rest_api.Controllers.FundController
{
    [Route("/api/[controller]")]
    public class PaymentTypeController : Controller
    {


        private readonly IPaymentTypeService _paymentTypeService;

        public PaymentTypeController(IPaymentTypeService paymentTypeService)
        {
            _paymentTypeService = paymentTypeService;
        }

        [HttpGet]
        public async Task<IEnumerable<PaymentType>> GetAllAsync()
        {
            var paymentTypes = await _paymentTypeService.ListAsync();

            return paymentTypes;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync(int id)
        {
            var paymentType = await _paymentTypeService.FindByIdAsync(id);

            return Ok(paymentType);
        }

        [HttpPost]
        //[ProducesResponseType(typeof(Country), 201)]
        //[ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostAsync([FromBody]PaymentType paymentType)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _paymentTypeService.SaveAsync(paymentType);
            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result);
            //return RedirectToAction("Index", "CountryClient");

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] PaymentType paymentType)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _paymentTypeService.UpdateAsync(id, paymentType);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(paymentType);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _paymentTypeService.DeleteAsync(id);

            if (!result.Success)

                return BadRequest(result.Message);

            return Ok(result);
        }

    }
}
