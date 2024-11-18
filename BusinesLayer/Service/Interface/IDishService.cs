using Restoran.Entity;

namespace Restoran.Service.Interface
{
    public interface IDishService
    {
        public Dish GetDishById(Guid dishid);
        public IEnumerable<Dish> GetAllDishes(Guid submenuid);
        public Dish AddNewDish(string dishesname, string dishesdescription, double dishescost, double dishescalories, Guid submenuid);
        public Dish ChangeDish(Guid dishesid, string newname, string newdeskription, double newcost, double newcalories);
        public bool DeleteDish(Guid dishid);
        public bool UnavailableDish(Guid dishid);
    }
}
