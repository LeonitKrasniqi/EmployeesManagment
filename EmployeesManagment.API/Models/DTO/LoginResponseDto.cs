namespace EmployeesManagment.API.Models.DTO
{
    //adding alone this DTO bc token should be secure
    public class LoginResponseDto
    {
        public string JwtToken { get; set; }    
    }
}
