using Microsoft.EntityFrameworkCore;
using SuperMarketComparison.Models;

namespace SuperMarketComparison.Data
{
    public class SMCContext : DbContext
    {
        public SMCContext(DbContextOptions<SMCContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // itemStorePrice to Item (many-to-one)
            modelBuilder.Entity<ItemStorePrice>()
                .HasOne(x => x.Item)
                .WithMany(x => x.Prices)
                .IsRequired(false);

            // CartItem to Cart (many-to-one)
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            // CartItem to ItemStorePrice (many-to-one)
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.ItemStorePrice)
                .WithMany()
                .HasForeignKey(ci => ci.ItemStorePriceId)
                .OnDelete(DeleteBehavior.Cascade);

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

            // SEED ITEM create test CART on db
            modelBuilder.Entity<Cart>().HasData(
                new Cart { 
                    Id=1,
                    CreatedAt= new DateTime(2025,8,9,0,0,0, DateTimeKind.Utc),
                    CompletedAt= null
                });

            // SEED ITEM create CARTITEM on db
            modelBuilder.Entity<CartItem>().HasData(
                new CartItem
                {
                    Id = 1,
                    CartId = 1,
                    ItemStorePriceId = 1,
                    IsChecked = false
                });
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<ItemStorePrice> ItemStorePrices { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet <CartItem> CartItems { get; set; }
    }
}
