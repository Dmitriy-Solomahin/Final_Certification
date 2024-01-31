namespace ApiUsers.Models
{
    public class User
    {
        Guid Id { get; set; }
        public UserName Email { get; set; }
        public string Password { get; set; }
        public Roles Role{ get; set; }

    }
}
