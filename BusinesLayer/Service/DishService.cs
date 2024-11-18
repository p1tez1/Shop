using FluentValidation;
using Restoran.Entity;
using Restoran.Service.Interface;

namespace Restoran.Service
{
    public class DishService : IDishService
    {
        private readonly ApplicationDbContext _context;
        private readonly IValidator<Dish> _dishValidator;

        public DishService(ApplicationDbContext context, IValidator<Dish> dishValidator)
        {
            _context = context;
            _dishValidator = dishValidator;
        }

        public Dish GetDishById(Guid dishId)
        {
            var dish = _context.Dishes.FirstOrDefault(d => d.Id == dishId) ?? throw new Exception("Dish not found");
            return dish;
        }

        public IEnumerable<Dish> GetAllDishes(Guid submenuId)
        {
            return _context.Dishes.Where(d => d.SubMenuId == submenuId).ToList();
        }

        public Dish AddNewDish(string dishesName, string dishesDescription, double dishesCost, double dishesCalories, Guid submenuId)
        {
            var dish = new Dish(dishesName, dishesDescription, dishesCost, dishesCalories, submenuId);
            ValidateDish(dish);

            _context.Dishes.Add(dish);
            _context.SaveChanges();

            return dish;
        }

        public Dish ChangeDish(Guid dishesId, string newName, string newDescription, double newCost, double newCalories)
        {
            var dish = GetDishById(dishesId);

            dish.NameDishes = newName;
            dish.Description = newDescription;
            dish.Cost = newCost;
            dish.Calories = newCalories;
            ValidateDish(dish);

            _context.SaveChanges();

            return dish;
        }

        public bool DeleteDish(Guid dishId)
        {
            var dish = GetDishById(dishId);
            _context.Dishes.Remove(dish);
            _context.SaveChanges();

            return true;
        }

        public bool UnavailableDish(Guid dishId)
        {
            var dish = GetDishById(dishId);

            dish.Available = false;
            _context.SaveChanges();

            return true;
        }

        private void ValidateDish(Dish dish)
        {
            var result = _dishValidator.Validate(dish);
            if (!result.IsValid)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Dish validation failed: {errors}");
            }
        }
    }
}
