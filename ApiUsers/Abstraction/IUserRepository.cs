using ApiUsers.Models;

namespace ApiUsers.Abstraction
{
    public interface IUserRepository
    {
        public User AddUser(UserDTO user);
        public IEnumerable<UserName> GetUsers();
        public int DeleteUser(UserName user);
    }
}
