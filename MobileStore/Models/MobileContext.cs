using Microsoft.EntityFrameworkCore;

namespace MobileStore.Models
{
    public class MobileContext : DbContext
    {
        public DbSet<Mobile> Mobiles { get; set; }

        public MobileContext(DbContextOptions<MobileContext> options)
            : base(options)
        {
        }
    }
}
