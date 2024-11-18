using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Restoran.Entity;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Menu> Menus { get; set; } 
    public DbSet<SubMenu> SubMenus { get; set; }
    public DbSet<Dish> Dishes { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<Receipt> Receipt { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Menu>()
            .HasMany(l => l.SubMenus)
            .WithOne(n => n.Menu)
            .HasForeignKey(n => n.MenuId);

        modelBuilder.Entity<SubMenu>()
            .HasMany(m => m.Dishes)
            .WithOne(d => d.SybMenu)
            .HasForeignKey(m => m.SubMenuId);

        modelBuilder.Entity<Receipt>()
            .HasMany(l => l.Orders)
            .WithOne(n => n.Receipt)
            .HasForeignKey(n => n.ReceiptId);

        modelBuilder.Entity<Order>()
            .Ignore(o => o.Dish)
            .Property(o => o.DishJson)
            .HasColumnName("Dish")
            .IsRequired();
    }
}
