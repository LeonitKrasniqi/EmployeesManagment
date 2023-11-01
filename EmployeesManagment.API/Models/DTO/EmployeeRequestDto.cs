using System.ComponentModel.DataAnnotations;

namespace EmployeesManagment.API.Models.DTO
{
    public class EmployeeRequestDto
    {
        public Guid Id { get; set; }
        [Required]
        public string? Position { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public int? ExperienceInMonth { get; set; }
        [Required]
        public string? Worktype { get; set; }
    }
}
