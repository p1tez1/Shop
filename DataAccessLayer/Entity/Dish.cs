

namespace Restoran.Entity
{
    public class Dish
    {
        public Guid Id { get; set; } 
        public string NameDishes { get; set; }
        public string Description { get; set; }
        public double Cost {  get; set; }
        public double Calories {  get; set; }
        public bool Available {  get; set; }
        public Guid SubMenuId { get; set; } 
        public virtual SubMenu SybMenu { get; set; }

        public Dish() { }
        
        public Dish(string namedishes, string description, double cost, double calories, Guid submenuid)
        {
            this.Id = new Guid();
            this.NameDishes = namedishes;
            this.Description = description;
            this.Cost = cost;
            this.Calories = calories;
            this.Available = true;
            this.SubMenuId = submenuid;
        }
    }
}
