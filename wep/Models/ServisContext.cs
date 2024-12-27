using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace wep.Models
{
    public class ServisContext : IdentityDbContext<UserDetails>
    {

        public DbSet<Servis> servis { get; set; }
        public DbSet<Employee> employee { get; set; }
        public DbSet<Rendezvou> rendezvou { get; set; }

        public ServisContext(DbContextOptions<ServisContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var admin = new IdentityRole("admin");
            admin.NormalizedName = "admin";

            var client = new IdentityRole("client");
            client.NormalizedName = "client";

            var employee = new IdentityRole("employee");
            employee.NormalizedName = "employee";

            builder.Entity<IdentityRole>().HasData(admin, client, employee);
        }

        public static async Task Seed(IServiceProvider serviceProvider, ServisContext context, UserManager<UserDetails> manager)
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                if (!context.employee.Any())
                {
                    context.employee.AddRange(
                        new Employee { EmployeeName = "Ayşe", EmployeeSurname = "Yıldız", EmployeeExperience = "Saç Kesmi", workingHours = 6 },//1
                        new Employee { EmployeeName = "Elif", EmployeeSurname = "Kara", EmployeeExperience = "Cılt Bakım", workingHours = 6 },//2
                        new Employee { EmployeeName = "Meryem", EmployeeSurname = "Demir", EmployeeExperience = "Pedükir", workingHours = 6 },//3
                        new Employee { EmployeeName = "Leyla", EmployeeSurname = "Altan", EmployeeExperience = "Makyaj", workingHours = 6 },//4
                        new Employee { EmployeeName = "Zahra", EmployeeSurname = "Toprak", EmployeeExperience = "Manükir", workingHours = 6 },//5
                        new Employee { EmployeeName = "Fatma", EmployeeSurname = "Koç", EmployeeExperience = "Saç Boyama", workingHours = 6 },//6
                        new Employee { EmployeeName = "Feyza", EmployeeSurname = "Özcan", EmployeeExperience = "Saç Fönleme", workingHours = 6 },//7
                        new Employee { EmployeeName = "Seda", EmployeeSurname = "Polat", EmployeeExperience = "Saç Dalgalama", workingHours = 6 }//8
                    );
                    context.SaveChanges();
                }

                if (!context.servis.Any())
                {
                    context.servis.AddRange(
                        new Servis { ServisName = "Saç Kesmi", ServisFee = 1000, EmployeeID = 1 },//1
                        new Servis { ServisName = "Cılt Bakım", ServisFee = 650, EmployeeID = 2 },//2
                        new Servis { ServisName = "Pedükir", ServisFee = 700, EmployeeID = 3 },//3
                        new Servis { ServisName = "Makyaj", ServisFee = 700, EmployeeID = 4 },//4
                        new Servis { ServisName = "Manükir", ServisFee = 750, EmployeeID = 5 },//5
                        new Servis { ServisName = "Saç Boyama", ServisFee = 1200, EmployeeID = 6 },//6
                        new Servis { ServisName = "Saç Fönleme", ServisFee = 1000, EmployeeID = 7 },//7
                        new Servis { ServisName = "Saç Dalgalama", ServisFee = 1000, EmployeeID = 8 }//8
                    );
                    context.SaveChanges();
                }

                var adminRole = context.Roles.First(r => r.NormalizedName == "admin");
                if (adminRole != null && !context.UserRoles.Any(u => u.RoleId == adminRole.Id))
                {
                    // Initialize default admin user
                    UserDetails admin = new UserDetails();
                    admin.Email = "admin@admin.com";
                    admin.UserName = "admin@admin.com";
                    admin.UserAd = "admin";
                    admin.UserSoyad = "1";

                    var result = await manager.CreateAsync(admin, "Admin@123");
                    if (result.Succeeded)
                    {
                        await manager.AddToRoleAsync(admin, "admin");
                        context.SaveChanges();
                    }
                }

                var clientRole = context.Roles.First(r => r.NormalizedName == "client");
                if (clientRole != null && !context.UserRoles.Any(u => u.RoleId == clientRole.Id))
                {
                    UserDetails client = new UserDetails();
                    // Initialize default admin user
                    client.Email = "test@test.com";
                    client.UserName = "test@test.com";
                    client.UserAd = "Test";
                    client.UserSoyad = "1";

                    var result = await manager.CreateAsync(client, "Test@123");
                    if (result.Succeeded)
                    {
                        await manager.AddToRoleAsync(client, "client");
                        context.SaveChanges();
                    }

                    Servis serv1 = context.servis.FirstOrDefault();
                    if (serv1 != null && client != null && !context.rendezvou.Any())
                    {
                        DateTime now = DateTime.Now;
                        DateTime customDateTiem = new DateTime(year: now.Year, month: now.Month, day: now.Day, hour: 15, minute: 0, second: 0);
                        context.rendezvou.AddRange(
                            new Rendezvou { RandezvouTime = customDateTiem, ServisID = serv1.ServisID, UserID = client.Id, user = client, servis = serv1 }
                        );
                        context.SaveChanges();
                    }
                }
                transaction.Commit();

            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }


        }

        // Call Seed method to insert data
        public static async Task Initialize(IServiceProvider serviceProvider, ServisContext context, UserManager<UserDetails> manager)
        {
            await Seed(serviceProvider, context, manager);
        }
    }
    public class UserDetails : IdentityUser
    {
        public string UserAd { get; set; }
        public string UserSoyad { get; set; }
    }
}


