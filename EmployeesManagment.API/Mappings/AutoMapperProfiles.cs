using AutoMapper;
using EmployeesManagment.API.Models.Domain;
using EmployeesManagment.API.Models.DTO;

namespace EmployeesManagment.API.Mappings
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<Employee, EmployeeRequestDto>().ReverseMap();
            CreateMap<EmployeeRequestDto, Employee>().ReverseMap();
            CreateMap<JobOfferRequestDto,JobOffer>().ReverseMap();
            CreateMap<JobOffer,JobOfferRequestDto>().ReverseMap();  

        }
    }
}
