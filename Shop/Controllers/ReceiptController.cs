using Microsoft.AspNetCore.Mvc;
using Restoran.Service.Interface;
using FluentValidation;
using FluentValidation.Results;
using Restoran.Entity;

namespace Restoran.Controllers
{
    [Route("api/receipt")]
    [ApiController]
    public class ReceiptController : ControllerBase
    {
        private readonly IReceiptService _receiptService;
        private readonly IValidator<Receipt> _receiptValidator;

        public ReceiptController(IReceiptService receiptService, IValidator<Receipt> receiptValidator)
        {
            _receiptService = receiptService;
            _receiptValidator = receiptValidator;
        }

        [HttpGet("GetReceiptById")]
        public ActionResult GetReceiptById(Guid receiptId)
        {
            var receipt = _receiptService.GetReceiptById(receiptId);
            return Ok(receipt);
        }

        [HttpGet("GetAllReceipt")]
        public ActionResult GetAllReceipt()
        {
            var receipts = _receiptService.GettAllReceipt();
            return Ok(receipts);
        }

        [HttpPost("PaidForOrder")]
        public ActionResult PaidForOrder(Guid orderId)
        {
            var receipt = _receiptService.PaidForOrder(orderId);
            ValidationResult result = _receiptValidator.Validate(receipt);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            return Ok(receipt);
        }

        [HttpPost("AddOrderToExistingReceipt")]
        public ActionResult AddOrderToExistingReceipt(Guid receiptId, Guid orderId)
        {
            var receipt = _receiptService.PaidForOrderInExistingReceipt(orderId, receiptId);
            ValidationResult result = _receiptValidator.Validate(receipt);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            return Ok(receipt);
        }

        [HttpDelete("DeleteReceipt")]
        public ActionResult DeleteReceipt(Guid receiptId)
        {
            var result = _receiptService.DeleteReceipt(receiptId);
            return Ok(result);
        }
    }
}
