using System.Text.Json;
using AutoMapper;
using FluentValidation;
using Restoran.Entity;
using Restoran.Service.Interface;

namespace Restoran.Service
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IValidator<Order> _orderValidator;
        private readonly IMapper _mapper;

        public OrderService(ApplicationDbContext context, IValidator<Order> orderValidator, IMapper mapper)
        {
            _context = context;
            _orderValidator = orderValidator;
            _mapper = mapper;
        }

        public Order CreateOrder()
        {
            var order = new Order();
           

            _context.Order.Add(order);
            _context.SaveChanges();

            return order;
        }

        public Order AddDishToExistingOrder(Guid orderId, Guid dishId)
        {
            var order = GetOrderById(orderId);
            var dish = _context.Dishes.FirstOrDefault(l => l.Id == dishId)?? throw new Exception("Dish do not found");

            if (order.Receipt == null)
            {
                var dishList = JsonSerializer.Deserialize<List<SimpleDish>>(order.DishJson) ?? new List<SimpleDish>();

                var simpleDish = dishList.FirstOrDefault(l => l.Id == dishId);

                if (simpleDish != null)
                {
                    simpleDish.Amount += 1;
                }
                else
                {
                    var simpdish = new SimpleDish(dishId, dish.NameDishes, dish.Cost, dish.Calories);
                    dishList.Add(simpdish);
                }

                order.DishJson = JsonSerializer.Serialize(dishList);
               

                _context.SaveChanges();

                return order;
            }
            else
            {
                throw new Exception("You can't change this order because it's already paid");
            }
        }

        public Order DeleteDishFromOrder(Guid orderId, Guid dishId)
        {
            var order = GetOrderById(orderId);

            if (order == null)
            {
                throw new Exception("Order not found");
            }

            if (order.Receipt == null)
            {
                var dishList = JsonSerializer.Deserialize<List<SimpleDish>>(order.DishJson) ?? new List<SimpleDish>();

                var dish = dishList.FirstOrDefault(l => l.Id == dishId);

                if (dish == null)
                {
                    throw new Exception("Dish not found");
                }

                if (dish.Amount > 1)
                {
                    dish.Amount -= 1;
                }
                else
                {
                    dishList.Remove(dish);
                }

                order.DishJson = JsonSerializer.Serialize(dishList);
                

                _context.SaveChanges();

                return order;
            }
            else
            {
                throw new Exception("You can't change this order because it's already paid");
            }
        }

        public bool DeleteOrder(Guid orderId)
        {
            var order = GetOrderById(orderId);
            if (order.Receipt == null)
            {
                _context.Order.Remove(order);
                _context.SaveChanges();

                return true;
            }
            else
            {
                throw new Exception("You can't change this order because it's already paid");
            }
        }

        public Order GetOrderById(Guid orderId)
        {
            var order = _context.Order.FirstOrDefault(l => l.Id == orderId) ?? throw new Exception();

            return order;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            var list = _context.Order.ToList();

            foreach (var order in list)
            {
                order.Dish = JsonSerializer.Deserialize<List<SimpleDish>>(order.DishJson) ?? new List<SimpleDish>();
            }

            return list;
        }

        private void ValidateOrder(Order order)
        {
            var result = _orderValidator.Validate(order);
            if (!result.IsValid)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Order validation failed: {errors}");
            }
        }
    }
}
