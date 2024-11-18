using Restoran.Entity;

namespace Restoran.Service.Interface
{
    public interface IOrderService
    {
        public Order CreateOrder();
        public Order AddDishToExistingOrder(Guid orderid, Guid dishid);
        public Order DeleteDishFromOrder(Guid orderid, Guid dishid);
        public bool DeleteOrder(Guid orderid);
        public Order GetOrderById(Guid orderid);
        public IEnumerable<Order> GetAllOrders();
    }
}
