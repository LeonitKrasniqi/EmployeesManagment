using System.ComponentModel.DataAnnotations;

namespace EmployeesManagment.API.Models.DTO
{
    public class JobOfferRequestDto
    {
        public Guid Id { get; set; }
        [Required]
        public string? Position { get; set; }
        [Required]
        public int? MinimumExperienceInMonth { get; set; }
        [Required]
        public string? WorkType { get; set; }
        [Required]
        public string? Description { get; set; }
    }
}
