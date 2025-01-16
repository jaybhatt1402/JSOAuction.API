using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using JSOAuction.API.Request.Payments;
using JSOAuction.Services.Entities.Payments;
using JSOAuction.Services.Interfaces;
using System.Threading.Tasks;

namespace JSOAuction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService, IMapper mapper)
        {
            _mapper = mapper;
            _paymentService = paymentService;
        }

        [HttpPost("SavePaymentData")]
        public async Task<IActionResult> SavePaymentData([FromBody] PaymentsRequest request)
        {
            if (request == null)
            {
                return BadRequest(new { Message = "Request body cannot be null" });
            }
            var paymentDto = _mapper.Map<PaymentsRequest, PaymentDto>(request);
            var result = await _paymentService.SavePaymentDataAsync(paymentDto);
            return Ok(result);
        }
    }
}
