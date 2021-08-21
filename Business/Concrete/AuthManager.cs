using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Result;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {

        IUserService _userService; 
        ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService,ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var operationClaims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateAccessToken(user, operationClaims.Data);
            return new SuccessDataResult<AccessToken>(accessToken,Messages.TokenCreated);
        }

        public IDataResult<User> Login(UserForLoginDto login)
        {
            var user = _userService.GetByEmail(login.Email);
            if (user.Data == null) return new ErrorDataResult<User>(Messages.UserNotFound);

            var isTrue = HashingHelper.VerifyPasswordHash(login.Password, user.Data.PasswordHash, user.Data.PasswordSalt);
            if (!isTrue) return new ErrorDataResult<User>(Messages.WrongPassword);

            return new SuccessDataResult<User>(user.Data, Messages.LoginSuccess);
        }

        public IDataResult<User> Register(UserForRegisterDto register)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(register.Password,out passwordHash,out passwordSalt);
            var user = new User
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                Email = register.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };

            _userService.Add(user);
            return new SuccessDataResult<User>(user, Messages.RegisterSuccess);
        }

        public IResult UserAlreadyExist(string email)
        {
            var result = _userService.GetByEmail(email);
            if (result.Data != null) return new ErrorResult(Messages.UserAlreadyExist);

            return new SuccessResult();
        }
    }
}
