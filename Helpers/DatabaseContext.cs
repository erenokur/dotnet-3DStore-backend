namespace dotnet_3D_store_backend.Helpers;

using Microsoft.EntityFrameworkCore;
using dotnet_3D_store_backend.Entities;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        Users = Set<Users>();
        Products = Set<Products>();
        Orders = Set<Orders>();
        OrderItems = Set<OrderItems>();
    }

    public DbSet<Users> Users { get; set; }

    public DbSet<Products> Products { get; set; }

    public DbSet<Orders> Orders { get; set; }

    public DbSet<OrderItems> OrderItems { get; set; }

    //public DbSet<Tasks> Tasks { get; set; }

}
