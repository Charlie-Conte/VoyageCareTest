using DapperExtensions.Mapper;

namespace StaffPortal.DataAccess.Core.Models
{
    public class CareHome
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Postcode { get; set; } = null!;
        public string AddressLine1 { get; set; } = null!;
        public string AddressLine2 { get; set; } = null!;
        public string? AddressLine3 { get; set; }
        public string? Landline { get; set; }
    }

    public sealed class CareHomeMapper : AutoClassMapper<CareHome>
    {
        public CareHomeMapper() : base()
        {
            Schema("Location");
            TableName = "CareHomes";
        }
    }
}
