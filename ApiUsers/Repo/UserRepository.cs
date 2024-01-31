using ApiUsers.Abstraction;
using ApiUsers.DB;
using ApiUsers.Models;

namespace ApiUsers.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly DBContext _context;
        public UserRepository(DBContext context)
        {
            _context = context;
        }


        public User AddUser(UserDTO user)
        {
            using (_context)
            {
                var entityUser = _context.Users.FirstOrDefault(x => x.Email.Equals(user.Email));
                if (entityUser == null)
                {
                    if (_context.Users.Count = 0)
                    {
                        entityUser = new User { Email = user.Email, Password = user.Password, Role = Roles.Admin };
                    }
                    else
                    {
                        entityUser = new User { Email = user.Email, Password = user.Password, Role = Roles.User };
                    }
                    _context.Users.Add(entityUser);
                    _context.SaveChanges();
                }
                else
                {
                    if (entityUser.Password != user.Password)
                        return null;
                }
                return entityUser;

            }
            
        }

        public int DeleteUser(UserName user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserName> GetUsers()
        {
            throw new NotImplementedException();
        }
    }
}
