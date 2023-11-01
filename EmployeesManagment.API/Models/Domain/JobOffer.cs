namespace EmployeesManagment.API.Models.Domain
{
    public class JobOffer
    {
        public Guid Id { get; set; }
        public string? Position { get; set; }
        public int? MinimumExperienceInMonth { get; set; }
        public string? WorkType { get; set; }
        public string? Description {  get; set; }

    }
}
