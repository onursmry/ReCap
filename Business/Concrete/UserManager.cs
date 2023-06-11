using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        IHttpContextAccessor _httpContextAccessor;

        public UserManager(IUserDal userDal,IHttpContextAccessor httpContextAccessor)
        {
            _userDal = userDal;
            _httpContextAccessor = httpContextAccessor;
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult(Messages.UserAdded);
        }

        public IResult Delete(User user)
        {
            _userDal.Add(user);
            return new SuccessResult(Messages.UserDeleted);
        }

        public IDataResult<User> GetById(int userId)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Id == userId));
        }

        public IDataResult<User> GetByMail(string email)
        {
            var checkedUser = _userDal.Get(u => u.Email == email);
            if (checkedUser != null)
            {
                return new SuccessDataResult<User>(checkedUser);
            }
            return new ErrorDataResult<User>();
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

        [ValidationAspect(typeof(UserValidator))]
        [Authentication]
        public IResult Update(UserForUpdateDto userForUpdateDto)
        {
            int authUserId = _httpContextAccessor.HttpContext.User.GetAuthenticatedUserId();
            var user = GetById(authUserId).Data;
            var verifyPassword = HashingHelper.VerifyPasswordHash(userForUpdateDto.OldPassword, user.PasswordHash, user.PasswordSalt);
            if (verifyPassword)
            {
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(userForUpdateDto.NewPassword, out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.LastName = userForUpdateDto.LastName;
                user.FirstName = userForUpdateDto.FirstName;
                _userDal.Update(user);
                return new SuccessResult(Messages.UserUpdated);
            }
            return new ErrorResult(Messages.PasswordError);
        }
    }
}
