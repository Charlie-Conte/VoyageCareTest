#nullable disable
using DapperExtensions.Mapper;

namespace StaffPortal.DataAccess.Core.Models
{
    public class Qualification
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string QualificationType { get; set; }
        public string ReceivedGrade { get; set; }
        public DateTime ReceivedDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
    public sealed class QualificationMapper : AutoClassMapper<Qualification>
    {
        public QualificationMapper() : base()
        {
            Schema("Staff");
            TableName = "Qualifications";
        }
    }
}
