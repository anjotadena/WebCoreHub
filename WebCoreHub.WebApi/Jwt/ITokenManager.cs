using WebCoreHub.Models;

namespace WebCoreHub.WebApi.Jwt
{
    public interface ITokenManager
    {
        string GenerateToken(User user);
    }
}
