namespace Restoran.Entity
{
    public class SubMenu
    {
        public Guid Id { get; set; } 
        public string Name { get; set; }
        public virtual IEnumerable<Dish> Dishes { get; set; }
        public virtual Menu Menu { get; set; }
        public Guid MenuId { get; set; }

        public SubMenu() { }
        public SubMenu (string name, Guid menuid)
        {
            this.Id = Guid.NewGuid ();
            this.Name = name;
            this.MenuId = menuid;
            this.Dishes = new List<Dish>();
        }
    } 
}
