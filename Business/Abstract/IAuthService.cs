using Core.Entities.Concrete;
using Core.Utilities.Result;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto register);
        IDataResult<User> Login(UserForLoginDto login);
        IResult UserAlreadyExist(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
