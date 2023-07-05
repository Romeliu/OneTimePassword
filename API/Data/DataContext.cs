using API.Domain;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DbSet<TemporaryPassword> TemporaryPasswords { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}