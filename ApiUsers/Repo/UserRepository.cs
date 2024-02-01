using ApiUsers.Abstraction;
using ApiUsers.DB;
using ApiUsers.Models;
using System.Security.Cryptography;
using System.Text;

namespace ApiUsers.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;
        public UserRepository(UserContext context)
        {
            _context = context;
        }


        public UserDTO AddUser(LoginModel user)
        {
            using (_context)
            {
                var entityUser = _context.Users.FirstOrDefault(x => x.Email.Equals(user.Email));
                if (entityUser == null)
                {
                    byte[] salt = new byte[16];
                    new Random().NextBytes(salt);

                    var data = Encoding.ASCII.GetBytes(user.Password).Concat(salt).ToArray();

                    SHA512 shaM = new SHA512Managed();

                    if (_context.Users.Count() == 0)
                    {
                        entityUser = new User
                        {
                            Email = user.Email,
                            Password = shaM.ComputeHash(data),
                            Salt = salt,
                            RoleId = Roles.Admin
                            
                        };
                    }
                    else
                    {
                        entityUser = new User
                        {
                            Email = user.Email,
                            Password = shaM.ComputeHash(data),
                            Salt = salt,
                            RoleId = Roles.User
                        };
                    }
                    _context.Users.Add(entityUser);
                    _context.SaveChanges();
                }
                else
                {
                    var data = Encoding.ASCII.GetBytes(user.Password).Concat(entityUser.Salt).ToArray();

                    SHA512 shaM = new SHA512Managed();
                    if (entityUser.Password != shaM.ComputeHash(data))
                        return null;
                }
                //maper
                var result = new UserDTO() { Email = entityUser.Email, Password = entityUser.Password.ToString(), Role = entityUser.RoleId };
                return result;

            }
            
        }

        public int DeleteUser(string user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetUsers()
        {
            throw new NotImplementedException();
        }
    }
}
