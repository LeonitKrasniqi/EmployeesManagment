using AutoMapper;
using EmployeesManagment.API.CustomActionFilters;
using EmployeesManagment.API.Data;
using EmployeesManagment.API.Models.Domain;
using EmployeesManagment.API.Models.DTO;
using EmployeesManagment.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesManagment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobOfferController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly EmployeesManagmentDbContext dbContext;
        private readonly IJobOfferRepository jobOfferRepository;

        public JobOfferController(IMapper mapper, EmployeesManagmentDbContext dbContext, IJobOfferRepository jobOfferRepository)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.jobOfferRepository = jobOfferRepository;
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles ="JobOffer")]
        public async Task<IActionResult> Create([FromBody] JobOfferRequestDto jobOfferRequestDto)
        {
            var jobOfferDomainModel = mapper.Map<JobOffer>(jobOfferRequestDto);
            jobOfferDomainModel = await jobOfferRepository.CreateAsync(jobOfferDomainModel);

            var jobOfferDto = mapper.Map<JobOfferRequestDto>(jobOfferDomainModel);
            return CreatedAtAction(nameof(GetById), new { id = jobOfferDto.Id }, jobOfferDto);
        }



       
        [HttpGet]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles ="JobOffer")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var jobOfferDomainModel = await jobOfferRepository.GetByIdAsync(id);
            if(jobOfferDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<JobOffer, JobOfferRequestDto>(jobOfferDomainModel));
        }
      

        [HttpGet]
        [ValidateModel]
        [Authorize(Roles ="Employee")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery]bool? isAscending )
        {
            var jobOfferDomainModel = await jobOfferRepository.GetAllAsync(filterOn,filterQuery, sortBy, isAscending??true);
            return Ok(mapper.Map<List<JobOfferRequestDto>>(jobOfferDomainModel));
        }
        

        [HttpDelete]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles ="JobOffer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var jobOfferDomainModel = await jobOfferRepository.DeleteAsync(id);

            if (jobOfferDomainModel == null)
            {
                return NotFound();
            }

            return Ok("Job doesnt exist anymore");
        }
    }
}
