using WebCoreHub.Models;

namespace WebCoreHub.Dal
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly WebCoreHubDbContext _dbContext;

        public AuthenticationRepository(WebCoreHubDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User? CheckCredentials(User user)
        {
            var userCredentials = _dbContext.Users.SingleOrDefault(u => u.Email == user.Email);

            return userCredentials;
        }

        public string GetUserRole(int roleId)
        {
            return _dbContext.Roles.SingleOrDefault(role => role.RoleId == roleId).RoleName;
        }

        public int RegisterUser(User user)
        {
            _dbContext.Users.Add(user);

            return _dbContext.SaveChanges();
        }
    }
}
