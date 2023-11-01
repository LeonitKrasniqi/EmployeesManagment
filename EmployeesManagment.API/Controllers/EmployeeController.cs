using AutoMapper;
using EmployeesManagment.API.CustomActionFilters;
using EmployeesManagment.API.Data;
using EmployeesManagment.API.Models.Domain;
using EmployeesManagment.API.Models.DTO;
using EmployeesManagment.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeesManagment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeesManagmentDbContext dbContext;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMapper mapper;

        public EmployeeController(EmployeesManagmentDbContext dbContext, IEmployeeRepository employeeRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
        }


        [HttpPost]
        [ValidateModel]
        [Authorize(Roles ="Employee")]
        public async Task<IActionResult> Create([FromBody] EmployeeRequestDto employeeRequestDto)
        {

            var employeeDomainModel = mapper.Map<Employee>(employeeRequestDto);

               employeeDomainModel = await employeeRepository.CreateAsync(employeeDomainModel);

            var employeeDto = mapper.Map<EmployeeRequestDto>(employeeDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = employeeDto.Id }, employeeDto);
            }
         




        [HttpGet]
        [ValidateModel]
        [Authorize(Roles ="JobOffer")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending)
        {

            var employeesDomain = await employeeRepository.GetAllAsync(filterOn,filterQuery,sortBy,isAscending??true);

            return Ok(mapper.Map<List<EmployeeRequestDto>>(employeesDomain));
        }


        [HttpGet]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles ="Employee")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)

        {
            var employeeDomain = await employeeRepository.GetByIdAsync(id);

            if (employeeDomain == null)
            {
                return NotFound();
            }

       

            return Ok(mapper.Map<Employee, EmployeeRequestDto>(employeeDomain));


        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles ="Employee")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            var employeeDomainModel = await employeeRepository.DeleteAsync(id);
            if(employeeDomainModel == null)
            {
                return NotFound();
            }

            return Ok("User was deleted!");

        }
    } 
}
