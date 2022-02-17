using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using WebApplication.Data.Entity;

namespace WebApplication.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Pizzeria> Pizzerias { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Addish> Addishes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<PizzaInMenu> Pizzas { get; set; }
        public DbSet<PizzaSize> PizzaSizes { get; set; }
        public DbSet<Client> Clients { get; set; }

        //public DbSet<ChosenAddish> ChosenAddishes { get; set; }
        //public DbSet<ChosenPizza> ChosenPizzas { get; set; }
        public DbSet<ClientOrder> ClientOrders { get; set; }

        public ApplicationContext( DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=PizzaWeb;Username=postgres;Password=123");
        //}
    }
}
