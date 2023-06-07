#nullable disable

namespace StaffPortal.DataAccess.Core.Models
{
    public class Qualifications
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string QualificationType { get; set; }
        public string ReceivedGrade { get; set; }
        public DateTime ReceivedDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
