using AutoMapper;
using JSOAuction.API.Request.Payments;
using JSOAuction.Services.Entities.Payment;
using JSOAuction.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JSOAuction.API.Controllers
{
    [Route("api/send-payment-link")]
    [ApiController]
    public class SendPaymentLinkController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISendLinkPaymentService _sendLinkPaymentService;

        public SendPaymentLinkController(ISendLinkPaymentService sendLinkPaymentService, IMapper mapper)
        {
            _mapper = mapper;
            _sendLinkPaymentService = sendLinkPaymentService;
        }

        [HttpPost("send-link")]
        public async Task<IActionResult> SavePaymentData([FromBody] SendLinkRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid request data" });

            try
            {
                var sendLinkDto = _mapper.Map<SendLinkRequest, SendLinkDto>(request);
                var result = await _sendLinkPaymentService.SendLinkPaymentDataAsync(sendLinkDto);

                if (result == null)
                    return BadRequest(new { message = "Failed to send payment link" });

                return Ok(new { message = "Payment link sent successfully", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }
    }
}