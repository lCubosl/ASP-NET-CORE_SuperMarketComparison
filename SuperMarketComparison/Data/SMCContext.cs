using Microsoft.EntityFrameworkCore;
using SuperMarketComparison.Models;

namespace SuperMarketComparison.Data
{
    public class SMCContext : DbContext
    {
        public SMCContext(DbContextOptions<SMCContext> options) : base(options) { }
        public DbSet<Item> Items { get; set; }
    }
}
