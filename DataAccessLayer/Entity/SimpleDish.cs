namespace Restoran.Entity
{
    public class SimpleDish
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Cost {  get; set; }
        public double Cal {  get; set; }
        public int Amount { get; set; }

        public SimpleDish(Guid id, string name, double cost,double cal) 
        {
            this.Id = id;
            this.Name = name;
            this.Cost = cost;
            this.Cal = cal;
            this.Amount = 1;
        }
    }
}
