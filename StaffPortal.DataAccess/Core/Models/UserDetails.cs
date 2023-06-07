#nullable disable

namespace StaffPortal.DataAccess.Core.Models
{
    public class UserDetails
    {
        public Guid Id { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string JobTitle { get; set; }
        public decimal AnnualSalary { get; set; }
    }
}
