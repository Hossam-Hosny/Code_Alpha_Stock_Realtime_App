using Microsoft.EntityFrameworkCore;

namespace Stock_Realtime_App.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
       public DbSet<Stock> Stocks { get; set; }
    }
}
