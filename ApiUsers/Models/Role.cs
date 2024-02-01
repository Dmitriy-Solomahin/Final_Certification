namespace ApiUsers.Models
{
    public class Role
    {
        public Roles RoleId { get; set; }
        public string RoleName { get; set; }
        public virtual List<User> Users { get; set; }
    }
}
