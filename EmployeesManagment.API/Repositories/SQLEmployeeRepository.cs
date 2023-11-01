using EmployeesManagment.API.Data;
using EmployeesManagment.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EmployeesManagment.API.Repositories
{
    public class SQLEmployeeRepository:IEmployeeRepository
    {
        private readonly EmployeesManagmentDbContext dbContext;

        public SQLEmployeeRepository(EmployeesManagmentDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Employee> CreateAsync(Employee employee)
        {
            await dbContext.Employees.AddAsync(employee);
            await dbContext.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee?> DeleteAsync(Guid id)
        {
            var existingEmployee = await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if(existingEmployee == null)
            {
                return null;
            }

             dbContext.Employees.Remove(existingEmployee);
            await dbContext.SaveChangesAsync();
            return existingEmployee;
        }

        public async Task<List<Employee>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true)
        {

            var employees = dbContext.Employees.AsQueryable();

            //Filtering data based on: Position, WorkType and Description
            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if(filterOn.Equals("Position", StringComparison.OrdinalIgnoreCase))
                {
                    employees = employees.Where(x => x.Position.Contains(filterQuery));
                }
                if(filterOn.Equals("WorkType", StringComparison.OrdinalIgnoreCase))
                {
                    employees = employees.Where(x => x.WorkType.Contains(filterQuery));
                }
                if (filterOn.Equals("Description", StringComparison.OrdinalIgnoreCase))
                {
                    employees = employees.Where(x => x.Description.Contains(filterQuery));
                }
            }

            //Sorting data based on ExperienceInMonth
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(sortBy.Equals("ExperienceInMonth", StringComparison.OrdinalIgnoreCase))
                {
                    employees = isAscending ? employees.OrderBy(x=>x.ExperienceInMonth): employees.OrderByDescending(x=>x.ExperienceInMonth);
                }

            }



            return await employees.ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(Guid id)
        {
          return await dbContext.Employees.FirstOrDefaultAsync(x=>x.Id == id);
        }
    }
}
