using EmployeesManagment.API.Models.Domain;

namespace EmployeesManagment.API.Repositories
{
    public interface IJobOfferRepository
    {
        Task<List<JobOffer>> GetAllAsync(string? filterOn=null, string? filterQuery=null, string? sortBy = null, bool isAscending = true);
        Task<JobOffer?> GetByIdAsync(Guid id);
        Task<JobOffer> CreateAsync(JobOffer jobOffer);
        Task<JobOffer?> DeleteAsync(Guid id);

    }
}
