using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using proeduedge.DAL.Entities;
using proeduedge.Repository;

namespace proeduedge.Controllers
{
    [Route("api/proeduedge")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentController(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        [HttpPost("pay")]
        public async Task<IActionResult> Pay([FromBody] Payment payment)
        {
            var result = await _paymentRepository.Pay(payment);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
