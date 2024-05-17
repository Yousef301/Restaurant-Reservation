using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Entities.Views;
using RestaurantReservation.Db.Enums;

namespace RestaurantReservation.Db;

public class RestaurantReservationDbContext : DbContext
{
    // private readonly string _connectionString;

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Table> Tables { get; set; }
    public DbSet<ReservationDetails> ReservationDetails { get; set; }
    public DbSet<EmployeeDetails> EmployeeDetails { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost;Database=RestaurantReservationCore;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var positionConverter = new EnumToStringConverter<Position>();

        modelBuilder.Entity<Customer>(ent => { ent.HasIndex(e => e.Email).IsUnique(); });
        modelBuilder.Entity<Employee>().Property(e => e.Position).HasConversion(positionConverter);
        modelBuilder.Entity<ReservationDetails>().HasNoKey().ToView("ReservationDetails");
        modelBuilder.Entity<EmployeeDetails>().HasNoKey().ToView("EmployeeDetails");

        base.OnModelCreating(modelBuilder);
    }
}