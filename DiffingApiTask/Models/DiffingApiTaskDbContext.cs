using Microsoft.EntityFrameworkCore;

namespace DiffingApiTask.Models
{
    public class DiffingApiTaskDbContext : DbContext
    {
        public DiffingApiTaskDbContext(DbContextOptions<DiffingApiTaskDbContext> options) : base(options) { Database.EnsureCreated(); }
        public DbSet<Entry> Entries { get; set; }
    }
}
