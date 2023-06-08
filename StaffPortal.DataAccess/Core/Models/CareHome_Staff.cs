using DapperExtensions.Mapper;

namespace StaffPortal.DataAccess.Core.Models
{
    public class CareHome_Staff
    {
        public Guid Id { get; set; }
        public Guid CareHomeId { get; set; }
        public string UserId { get; set; } = null!;
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
