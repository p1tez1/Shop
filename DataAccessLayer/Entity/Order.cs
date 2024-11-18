using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;


namespace Restoran.Entity
{
    public class Order
    {
        public Guid Id { get; set; }
        public IEnumerable<SimpleDish> Dish;
        public DateTime Time { get; set; }
        public Guid? ReceiptId { get; set; }
        public virtual Receipt Receipt { get; set; }

        public string DishJson
        {
            get => JsonSerializer.Serialize(Dish);
            set => Dish = JsonSerializer.Deserialize<List<SimpleDish>>(value) ?? new List<SimpleDish>();
        }

        public Order()
        {
            this.Id = Guid.NewGuid();
            this.Dish = new List<SimpleDish>();
            this.Time = DateTime.Now;
            this.ReceiptId = null;
        }
    }
}
