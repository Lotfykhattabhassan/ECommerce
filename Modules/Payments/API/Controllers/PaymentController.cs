using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniECommerce.Modules.Payment.Application.Abstractions;
using MiniECommerce.Modules.Payment.Application.DTOs;

namespace MiniECommerce.Modules.Payment.API.Controllers
{
    [ApiController]
    [Route("api/payments")]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreatePaymentDto dto,
            CancellationToken cancellationToken)
        {
            var payment = await _paymentService.CreateAsync(
                dto,
                cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = payment.Id },
                payment);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var payment = await _paymentService.GetByIdAsync(
                id,
                cancellationToken);

            if (payment is null)
                return NotFound();

            return Ok(payment);
        }

        [HttpGet("order/{orderId:guid}")]
        public async Task<IActionResult> GetByOrderId(
            Guid orderId,
            CancellationToken cancellationToken)
        {
            var payment = await _paymentService.GetByOrderIdAsync(
                orderId,
                cancellationToken);

            if (payment is null)
                return NotFound();

            return Ok(payment);
        }

        [HttpGet("user/{userId:guid}")]
        public async Task<IActionResult> GetByUserId(
            Guid userId,
            CancellationToken cancellationToken)
        {
            var payments = await _paymentService.GetByUserIdAsync(
                userId,
                cancellationToken);

            return Ok(payments);
        }

        [HttpPatch("{id:guid}/processing")]
        public async Task<IActionResult> MarkAsProcessing(
            Guid id,
            CancellationToken cancellationToken)
        {
            var payment = await _paymentService.MarkAsProcessingAsync(
                id,
                cancellationToken);

            return Ok(payment);
        }

        [HttpPatch("{id:guid}/paid")]
        public async Task<IActionResult> MarkAsPaid(
            Guid id,
            [FromBody] MarkPaymentAsPaidDto dto,
            CancellationToken cancellationToken)
        {
            var payment = await _paymentService.MarkAsPaidAsync(
                id,
                dto,
                cancellationToken);

            return Ok(payment);
        }

        [HttpPatch("{id:guid}/failed")]
        public async Task<IActionResult> MarkAsFailed(
            Guid id,
            CancellationToken cancellationToken)
        {
            var payment = await _paymentService.MarkAsFailedAsync(
                id,
                cancellationToken);

            return Ok(payment);
        }

        [HttpPatch("{id:guid}/cancel")]
        public async Task<IActionResult> Cancel(
            Guid id,
            CancellationToken cancellationToken)
        {
            var payment = await _paymentService.CancelAsync(
                id,
                cancellationToken);

            return Ok(payment);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(
            Guid id,
            CancellationToken cancellationToken)
        {
            var payment = await _paymentService.DeleteAsync(
                id,
                cancellationToken);

            return Ok(payment);
        }
    }
}