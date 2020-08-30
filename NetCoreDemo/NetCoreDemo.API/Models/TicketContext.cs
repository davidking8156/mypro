using Microsoft.EntityFrameworkCore;

namespace NetCoreDemo.API.Models
{
    public class TicketContext : DbContext
    {
        public TicketContext(DbContextOptions<TicketContext> options) : base(options)
        {
        }
        public DbSet<TicketItem> TicketItems { get; set; }
    }
}
