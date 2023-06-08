#nullable disable
using DapperExtensions.Mapper;

namespace StaffPortal.DataAccess.Core.Models
{    
    public class UserDetail
    {
        public string Id { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string JobTitle { get; set; }
        public decimal AnnualSalary { get; set; }
    }
    public sealed class UserDetailMapper : AutoClassMapper<UserDetail>
    {
        public UserDetailMapper() : base()
        {
            Schema("Staff");
            TableName = "UserDetails";
        }
    }
}
