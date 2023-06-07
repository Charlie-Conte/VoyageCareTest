using StaffPortal.DataAccess.Auth;

namespace StaffPortal.DataAccess.Core.Models
{
    public class CareHome_Staffs
    {
        public Guid Id { get; set; }
        public CareHomes CareHomes { get; set; } = null!;
        public AspNetUsers AspNetUsers { get; set; } = null!;
    }
}
