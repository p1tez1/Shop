using Microsoft.AspNetCore.Mvc;
using Restoran.Service.Interface;
using FluentValidation;
using FluentValidation.Results;
using Restoran.Entity;

namespace Restoran.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IValidator<Order> _orderValidator;

        public OrderController(IOrderService orderService, IValidator<Order> orderValidator)
        {
            _orderService = orderService;
            _orderValidator = orderValidator;
        }

        [HttpGet("GetAllOrder")]
        public ActionResult GetAllOrder()
        {
            var allOrders = _orderService.GetAllOrders();
            return Ok(allOrders);
        }

        [HttpGet("GetOrderById")]
        public ActionResult GetOrderById(Guid orderId)
        {
            var orderById = _orderService.GetOrderById(orderId);
            return Ok(orderById);
        }

        [HttpPost("CreateOrder")]
        public ActionResult CreateOrder()
        {
            var createdOrder = _orderService.CreateOrder();
            return Ok(createdOrder);
        }

        [HttpPost("AddDishToExistingOrder")]
        public ActionResult AddDishToExistingOrder(Guid orderId, Guid dishId)
        {
            try
            {
                var order = _orderService.AddDishToExistingOrder(orderId, dishId);
                ValidationResult result = _orderValidator.Validate(order);

                if (!result.IsValid)
                {
                    return BadRequest(result.Errors);
                }

                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteDishesFromOrder")]
        public ActionResult DeleteDishesFromOrder(Guid orderId, Guid dishId)
        {
            var order = _orderService.DeleteDishFromOrder(orderId, dishId);
            return Ok(order);
        }

        [HttpDelete("DeleteOrder")]
        public ActionResult DeleteOrder(Guid orderId)
        {
            var order = _orderService.DeleteOrder(orderId);
            return Ok(true);
        }
    }
}
