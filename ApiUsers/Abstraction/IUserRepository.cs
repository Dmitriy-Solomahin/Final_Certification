using ApiUsers.Models;

namespace ApiUsers.Abstraction
{
    public interface IUserRepository
    {
        public UserDTO AddUser(LoginModel user);
        public IEnumerable<string> GetUsers();
        public int DeleteUser(string user);
    }
}
