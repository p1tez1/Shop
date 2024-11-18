using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Restoran.Entity;
using Restoran.Service.Interface;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Restoran.Service
{
    public class ReceiptService : IReceiptService
    {
        private readonly ApplicationDbContext _context;
        private readonly IValidator<Receipt> _receiptValidator;

        public ReceiptService(ApplicationDbContext context, IValidator<Receipt> receiptValidator)
        {
            _context = context;
            _receiptValidator = receiptValidator;
        }

        public Receipt GetReceiptById(Guid receiptId)
        {
            var rec = _context.Receipt.FirstOrDefault(l => l.Id == receiptId) ?? throw new Exception("Receipt not found");
            return rec;
        }

        public IEnumerable<Receipt> GettAllReceipt()
        {
            var listrec = _context.Receipt.Include(r => r.Orders).ToList();
            return listrec;
        }

        public Receipt PaidForOrder(Guid orderId)
        {
            var order = _context.Order.FirstOrDefault(l => l.Id == orderId) ?? throw new Exception("Order not found");

            if (order.Receipt != null)
            {
                throw new Exception("Order is already paid");
            }

            var dishList = JsonSerializer.Deserialize<List<SimpleDish>>(order.DishJson) ?? new List<SimpleDish>();

            double totalCost = 0;
            double totalCalories = 0;

            foreach (var dish in dishList)
            {
                totalCost += dish.Cost * dish.Amount;
                totalCalories += dish.Cal * dish.Amount;
            }

            var receipt = new Receipt(totalCost, totalCalories);
            receipt.Orders = new List<Order> { order };

            order.ReceiptId = receipt.Id;
            order.Receipt = receipt;

            ValidateReceipt(receipt);

            _context.Receipt.Add(receipt);
            _context.SaveChanges();

            return receipt;
        }

        public Receipt PaidForOrderInExistingReceipt(Guid orderId, Guid receiptId)
        {
            var order = _context.Order.FirstOrDefault(l => l.Id == orderId);
            var receipt = _context.Receipt.FirstOrDefault(l => l.Id == receiptId);

            if (order == null)
            {
                throw new Exception("Order not found");
            }

            if (receipt == null)
            {
                throw new Exception("Receipt not found");
            }

            if (order.Receipt != null)
            {
                throw new Exception("Order is already paid");
            }

            var dishList = JsonSerializer.Deserialize<List<SimpleDish>>(order.DishJson) ?? new List<SimpleDish>();

            double totalCost = receipt.Cost;
            double totalCalories = receipt.Calories;

            foreach (var dish in dishList)
            {
                totalCost += dish.Cost * dish.Amount;
                totalCalories += dish.Cal * dish.Amount;
            }

            receipt.Cost = totalCost;
            receipt.Calories = totalCalories;

            if (receipt.Orders == null)
            {
                receipt.Orders = new List<Order>();
            }

            var ordersList = receipt.Orders.ToList();
            ordersList.Add(order);
            receipt.Orders = ordersList;

            order.ReceiptId = receipt.Id;
            order.Receipt = receipt;

            ValidateReceipt(receipt);

            try
            {
                _context.Receipt.Update(receipt);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;
                if (innerException != null)
                {
                    throw new Exception($"Error saving changes: {ex.Message}. Inner exception: {innerException.Message}", ex);
                }
                else
                {
                    throw new Exception($"Error saving changes: {ex.Message}", ex);
                }
            }

            return receipt;
        }

        public bool DeleteReceipt(Guid receiptId)
        {
            var receipt = _context.Receipt.FirstOrDefault(r => r.Id == receiptId) ?? throw new Exception("Receipt not found");

            _context.Receipt.Remove(receipt);
            _context.SaveChanges();

            return true;
        }
        private void ValidateReceipt(Receipt receipt)
        {
            var result = _receiptValidator.Validate(receipt);
            if (!result.IsValid)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Receipt validation failed: {errors}");
            }
        }
    }
}
