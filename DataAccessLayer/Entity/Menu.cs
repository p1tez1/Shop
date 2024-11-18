namespace Restoran.Entity
{
    public class Menu
    {
        public Guid Id { get; set; }    
        public string Name { get; set; }
        public virtual IEnumerable<SubMenu> SubMenus { get; set; }

        public Menu() { }

        public Menu(string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.SubMenus = new List<SubMenu>();
        }
    }
}
