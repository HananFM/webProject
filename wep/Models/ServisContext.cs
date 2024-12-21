using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace wep.Models
{
    public class ServisContext: IdentityDbContext<UserDetails>
    {

        public DbSet<Servis> servis { get; set; }
        public DbSet<Employee> employee { get; set; }
        public DbSet<Rendezvou> rendezvou { get; set; }
        public DbSet<User> user { get; set; }

        public ServisContext(DbContextOptions<ServisContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var admin = new IdentityRole("admin");
            admin.NormalizedName= "admin";

            var client = new IdentityRole("client");
            client.NormalizedName = "client";

            var employee = new IdentityRole("employee");
            employee.NormalizedName = "employee";

            builder.Entity<IdentityRole>().HasData(admin, client, employee);
        }
    }
    public class UserDetails : IdentityUser
    {
        public string UserAd { get; set; }
        public string UserSoyad { get; set; }
    }
}


