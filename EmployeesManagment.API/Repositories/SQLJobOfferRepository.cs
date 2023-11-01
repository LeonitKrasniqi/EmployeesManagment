using EmployeesManagment.API.Data;
using EmployeesManagment.API.Models.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace EmployeesManagment.API.Repositories
{
    public class SQLJobOfferRepository : IJobOfferRepository
    {
        private readonly EmployeesManagmentDbContext dbContext;

        public SQLJobOfferRepository(EmployeesManagmentDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<JobOffer> CreateAsync(JobOffer jobOffer)
        {
            await dbContext.JobOffer.AddAsync(jobOffer);
            await dbContext.SaveChangesAsync();
            return jobOffer;
        }

        public async Task<JobOffer?> DeleteAsync(Guid id)
        {
            var jobOfferExist = await dbContext.JobOffer.FirstOrDefaultAsync(x=>x.Id == id);
            if (jobOfferExist == null)
            {
                return null;
            }

            dbContext.JobOffer.Remove(jobOfferExist);
            await dbContext.SaveChangesAsync();
            return jobOfferExist;
        }

        public  async Task<List<JobOffer>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
             string? sortBy = null, bool isAscending = true)
        {
            var jobOffers = dbContext.JobOffer.AsQueryable();

            //filtering data based on fields: Position, WorkType and Description
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Position", StringComparison.OrdinalIgnoreCase))
                {
                    jobOffers = jobOffers.Where(x => x.Position.Contains(filterQuery));
                }
                if (filterOn.Equals("WorkType", StringComparison.OrdinalIgnoreCase))
                {
                    jobOffers = jobOffers.Where(x => x.WorkType.Contains(filterQuery));
                }
                if (filterOn.Equals("Description", StringComparison.OrdinalIgnoreCase))
                {
                    jobOffers = jobOffers.Where(x => x.Description.Contains(filterQuery));
                }

            }

            //sorting data based on MinimumExpirienceInMonth
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("MinimumExperienceInMonth", StringComparison.OrdinalIgnoreCase))
                {
                    jobOffers = isAscending ? jobOffers.OrderBy(x => x.MinimumExperienceInMonth) : jobOffers.OrderByDescending(x => x.MinimumExperienceInMonth);
                }

            }


            return await jobOffers.ToListAsync();
        }

        public async Task<JobOffer?> GetByIdAsync(Guid id)
        {
            return await dbContext.JobOffer.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
