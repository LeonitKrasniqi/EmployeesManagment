using System.ComponentModel.DataAnnotations;

namespace EmployeesManagment.API.Models.DTO
{
    public class RegisterRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //adding roles as a array
        public string[] Roles { get; set; }
    }
}
