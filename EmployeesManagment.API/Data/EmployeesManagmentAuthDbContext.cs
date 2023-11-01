using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeesManagment.API.Data
{
    public class EmployeesManagmentAuthDbContext : IdentityDbContext
    {
        public EmployeesManagmentAuthDbContext(DbContextOptions<EmployeesManagmentAuthDbContext> options): base(options)
        {

            
        }

        //seeding roles
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //two valid IDs for each role
            var employeeId = "ecbbf4be-7ff4-4fba-b104-d63991b6f0b6";
            var jobOfferId = "6b3f9ad8-4740-4a23-b6e8-6177d8b7c31c";


            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = employeeId,
                    ConcurrencyStamp=employeeId,
                    Name="Employee",
                    NormalizedName="Employee".ToUpper(),
                },
                new IdentityRole {
                    Id = jobOfferId,
                    ConcurrencyStamp=jobOfferId,
                    Name="JobOffer",
                    NormalizedName="JobOffer".ToUpper(),
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
