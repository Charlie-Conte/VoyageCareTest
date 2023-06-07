using DapperExtensions.Mapper;
using StaffPortal.DataAccess.Core.Models;

namespace StaffPortal.DataAccess.Auth
{
    public class AspNetUser
    {
        public string Id { get; set; } = null!;
        public string? UserName { get; set; }
        public string? NormalizedUserName { get; set; }
        public string? Email { get; set; }
        public string? NormalizedEmail { get; set; }
        public string? PhoneNumber { get; set; }
    }

    public sealed class AspNetUserMapper : AutoClassMapper<AspNetUser>
    {
        public AspNetUserMapper() : base()
        {
            Schema("dbo");
            TableName = "AspNetUsers";
        }
    }
}
