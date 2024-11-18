using Microsoft.AspNetCore.Mvc;
using Restoran.Service.Interface;
using FluentValidation;
using FluentValidation.Results;
using Restoran.Entity;


namespace Restoran.Controllers
{
    [Route("api/dish")]
    [ApiController]
    public class DishesController : ControllerBase
    {
        private readonly IDishService _dishService;
        private readonly IValidator<Dish> _dishValidator;

        public DishesController(IDishService dishService, IValidator<Dish> dishValidator)
        {
            _dishService = dishService;
            _dishValidator = dishValidator;
        }

        [HttpGet("GetDishById")]
        public ActionResult GetDishById(Guid dishId)
        {
            var dish = _dishService.GetDishById(dishId);
            return Ok(dish);
        }

        [HttpGet("GetAllDishes")]
        public ActionResult GetAllDishes(Guid submenuId)
        {
            var dishes = _dishService.GetAllDishes(submenuId);
            return Ok(dishes);
        }

        [HttpPost("AddNewDish")]
        public ActionResult AddNewDish(string dishesName, string dishesDescription, double dishesCost, double dishesCalories, Guid submenuId)
        {
            var dish = new Dish(dishesName, dishesDescription, dishesCost, dishesCalories, submenuId);
            ValidationResult result = _dishValidator.Validate(dish);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var newDish = _dishService.AddNewDish(dishesName, dishesDescription, dishesCost, dishesCalories, submenuId);
            return Ok(newDish);
        }

        [HttpPut("ChangeDish")]
        public ActionResult ChangeDish(Guid dishesId, string newName, string newDescription, double newCost, double newCalories)
        {
            var dish = _dishService.GetDishById(dishesId);
            dish.NameDishes = newName;
            dish.Description = newDescription;
            dish.Cost = newCost;
            dish.Calories = newCalories;

            ValidationResult result = _dishValidator.Validate(dish);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var updatedDish = _dishService.ChangeDish(dishesId, newName, newDescription, newCost, newCalories);
            return Ok(updatedDish);
        }

        [HttpDelete("DeleteDish")]
        public ActionResult DeleteDish(Guid dishId)
        {
            var result = _dishService.DeleteDish(dishId);
            return Ok(result);
        }

        [HttpPut("ChangeAvailabilityDish")]
        public ActionResult UnavailableDish(Guid dishId)
        {
            var result = _dishService.UnavailableDish(dishId);
            return Ok(result);
        }
    }
}
