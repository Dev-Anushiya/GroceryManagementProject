using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using GroceryManSystem.Models;

public class GroceryDbContext : DbContext
{
    public GroceryDbContext() : base("GroceryDbContext") { }
    public DbSet<User> Users { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<PackingPerson> PackingPersons { get; set; }
    public DbSet<DeliveryPerson> DeliveryPersons1 { get; set; }

}
