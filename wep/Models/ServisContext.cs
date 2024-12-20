using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace wep.Models
{
    public class ServisContext: IdentityDbContext
    {

        public DbSet<Servis> servis { get; set; }
        public DbSet<Employee> employee { get; set; }
        public DbSet<Rendezvou> rendezvou { get; set; }
        public DbSet<User> user { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb; Database=KuaforSalonu2  ;Trusted_Connection=True;");
        }
    }
}
