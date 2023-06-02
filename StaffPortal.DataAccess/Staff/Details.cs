namespace StaffPortal.DataAccess.Staff
{
    public class User
    {
        public Guid Id { get; set; }
        public int AspId { get; set; }
        public string Name { get; set; } = null!;
    }
}
