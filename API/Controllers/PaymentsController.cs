using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;
using Application.IServices;
using Domain.Common;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly ILogger<PaymentsController> _logger;
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService, ILogger<PaymentsController> logger)
        {
            _logger = logger;
            _paymentService = paymentService;
        }

        [HttpPost]
        public IActionResult CreatePayment([FromBody] PaymentDTO paymentDTO)
        {
            Result<Payment> result = _paymentService.CreatePayment(paymentDTO);

            if (result.IsFailure)
            {
                _logger.LogError("Couldn't make payment. Error message: {Message}", result.Error);
                return Problem(null, null, 500, result.Error);
            }   
            else
            {
                _logger.LogInformation("Payment with id {PaymentId} is made for order with id {OrderId}", result.Value.Id, result.Value.OrderId);
                
                string url = this.GetUrl("/api/payments/" + result.Value.Id);
                return Created(url, result.Value);
            }
        }
    }
}