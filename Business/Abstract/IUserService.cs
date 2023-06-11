using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<OperationClaim>> GetClaims(User user);
        IResult Add(User user);
        IResult Delete(User user);
        IDataResult<User> GetById(int userId);
        IResult Update(UserForUpdateDto userForUpdateDto);
        IDataResult<User> GetByMail(string email);

    }
}
