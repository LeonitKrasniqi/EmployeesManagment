using System.ComponentModel.DataAnnotations;

namespace EmployeesManagment.API.Models.Domain
{
    public class Employee
    {

        public Guid Id { get; set; }  
        public string? Position { get; set; }
        public string? Description { get; set; }
        public int? ExperienceInMonth { get; set; }
        public string? WorkType {  get; set; }


    }
}
