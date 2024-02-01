namespace ApiUsers.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; } 
        public Roles RoleId { get; set; }
        public Role Role { get; set; }
    }
}
