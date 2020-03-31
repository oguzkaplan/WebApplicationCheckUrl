using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<User>> GetUserList();

        IDataResult<User> GetUserById(int id);

        IDataResult<User> GetUserLogin(string mailAddress, string password);

        IResult Save(User user);

        IResult Update(User user);

        IResult Delete(User user);


    }
}
