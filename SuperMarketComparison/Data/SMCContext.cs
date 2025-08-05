using Microsoft.EntityFrameworkCore;
using SuperMarketComparison.Models;

namespace SuperMarketComparison.Data
{
    public class SMCContext : DbContext
    {
        public SMCContext(DbContextOptions<SMCContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemStorePrice>()
                .HasOne(x => x.Item)
                .WithMany(x => x.Prices)
                .IsRequired(false);

            // SEED ITEM create test STORE model on db
            modelBuilder.Entity<Store>().HasData(
                new Store { Id=1, Name="Test Store", Location= "Location 1" });

            // SEED ITEM create ITEMSTOREPRICE on db
            modelBuilder.Entity<ItemStorePrice>().HasData(
                new ItemStorePrice
                {
                    Id=1,
                    ItemId=1,
                    StoreId=1,
                    Price=1.00m,
                    LastUpdate= new DateTime(2025,8,4)
                });
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<ItemStorePrice> ItemStorePrices { get; set; }
    }
}
