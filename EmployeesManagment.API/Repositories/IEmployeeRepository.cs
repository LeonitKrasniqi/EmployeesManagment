using EmployeesManagment.API.Models.Domain;

namespace EmployeesManagment.API.Repositories
{
    public interface IEmployeeRepository
    {

        Task<List<Employee>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true);

        Task<Employee?> GetByIdAsync(Guid id);

        Task<Employee> CreateAsync(Employee employee);
        Task<Employee?> DeleteAsync(Guid id);

    }
}
