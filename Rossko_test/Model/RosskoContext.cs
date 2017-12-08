using Microsoft.EntityFrameworkCore;

namespace Rossko_test.Model
{
    public class RosskoContext : DbContext
    {
        public DbSet<OptionsArray> OptionsArrays { get; set; }

        public RosskoContext(DbContextOptions<RosskoContext> options)
            : base(options)
        {

        }
    }
}
