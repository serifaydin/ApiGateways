using MyOcelot.Services.Auth.Api.Models;

namespace MyOcelot.Services.Auth.Api.Middleware
{
    public interface IJWTManager
    {
        UserModel AuthenticateUser(UserModel model);
        string GenerateJSONWebToken(UserModel userInfo);
    }
}