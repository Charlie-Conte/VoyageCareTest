using StaffPortal.DataAccess.Auth;
using DapperExtensions.Mapper;

namespace StaffPortal.DataAccess.Core.Models
{
    public class CareHome_Staff
    {
        public Guid Id { get; set; }
        public Guid CareHomeId { get; set; }
        public Guid UserId { get; set; }
    }
    public sealed class CareHome_StaffMapper : AutoClassMapper<CareHome_Staff>
    {
        public CareHome_StaffMapper() : base()
        {
            Schema("Location");
            TableName = "CareHome_Staff";
            AutoMap();
        }
    }
}
