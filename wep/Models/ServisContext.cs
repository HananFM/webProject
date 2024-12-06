using Microsoft.EntityFrameworkCore;

namespace wep.Models
{
    public class ServisContext: DbContext
    {
        public DbSet<Servis> servis { get; set; }
        public DbSet<Employee> employee { get; set; }

        internal static Task<string?> ToListAsync()
        {
            throw new NotImplementedException();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb; Database=KuaforSalonu  ;Trusted_Connection=True;");
        }
    }
}
