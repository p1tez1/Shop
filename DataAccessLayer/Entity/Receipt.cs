namespace Restoran.Entity
{
    public class Receipt
    {
        public Guid Id { get; set; }
        public double Cost { get; set; }
        public double Calories {  get; set; }
        public DateTime Time { get; set; }
        public virtual IEnumerable<Order> Orders {get; set;}

        public Receipt() { }
        public Receipt(double cost, double calories)
        {
            this.Id = Guid.NewGuid();
            this.Cost = cost;
            this.Calories = calories;
            this.Time = DateTime.Now;
            this.Orders = new List<Order> ();
        }
    }
}
