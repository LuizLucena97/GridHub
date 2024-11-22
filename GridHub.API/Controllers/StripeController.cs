using GridHub.Service.Payment;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace GridHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeController : ControllerBase
    {
        [HttpPost("create-payment-intent")]
        public IActionResult CreatePaymentIntent([FromBody] PaymentRequest paymentRequest)
        {
            try
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = paymentRequest.Amount,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" },
                };
                var service = new PaymentIntentService();
                PaymentIntent intent = service.Create(options);

                return Ok(new { clientSecret = intent.ClientSecret });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }

    public class PaymentRequest
    {
        public long Amount { get; set; }
    }
}

