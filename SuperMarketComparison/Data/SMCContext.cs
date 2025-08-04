using Microsoft.EntityFrameworkCore;
using SuperMarketComparison.Models;

namespace SuperMarketComparison.Data
{
    public class SMCContext : DbContext
    {
        public SMCContext(DbContextOptions<SMCContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // create test STORE model on db
            modelBuilder.Entity<Store>().HasData(
                new Store { Id=1, Name="Test Store", Location= "Location 1" });
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Store> Stores { get; set; }
    }
}
