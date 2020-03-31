using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class UserService : IUserService
    {
        private IUserDal _userdal;
        public UserService(IUserDal userDal)
        {
            _userdal = userDal;
        }
        public IResult Delete(User user)
        {
            _userdal.Delete(user);
            return new SuccessResult();
        }
        public IResult Save(User user)
        {
            _userdal.Add(user);
            return new SuccessResult();
        }

        public IResult Update(User user)
        {
            _userdal.Update(user);
            return new SuccessResult();
        }
        public IDataResult<User> GetUserById(int id)
        {
            return new SuccessDataResult<User>(_userdal.Get(filter: p => p.Id == id));
        }

        public IDataResult<List<User>> GetUserList()
        {
            return new SuccessDataResult<List<User>>(_userdal.GetList(null).ToList());
        }


        public IDataResult<User> GetUserLogin(string mailAddress, string password)
        {
            return new SuccessDataResult<User>(_userdal.GetList(a => a.MailAddress == mailAddress &&
            a.Password == password && a.IsActive
            == 1).FirstOrDefault());
        }


    }
}
