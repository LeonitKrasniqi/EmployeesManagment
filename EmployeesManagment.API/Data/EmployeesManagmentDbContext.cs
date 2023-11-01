using EmployeesManagment.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EmployeesManagment.API.Data
{
    public class EmployeesManagmentDbContext : DbContext
    {
        public EmployeesManagmentDbContext(DbContextOptions<EmployeesManagmentDbContext> dbContextOptions ) : base(dbContextOptions)
        {
                
        }

        //Db set, property of DbContext class that represents a collection of entities in the database
         
        public DbSet<Employee> Employees { get; set; }
        public DbSet<JobOffer> JobOffer { get; set; }
    }
}
