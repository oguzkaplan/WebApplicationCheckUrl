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
    public class UrlService : IUrlService
    {
        private IUrlDal _urlDal;
        public UrlService(IUrlDal urlDal)
        {
            _urlDal = urlDal;
        }
        public IResult Delete(Url url)
        {
            _urlDal.Delete(url);
            return new SuccessResult();
        }
        public IResult Save(Url url)
        {
            _urlDal.Add(url);
            return new SuccessResult();
        }

        public IResult Update(Url url)
        {
            _urlDal.Update(url);
            return new SuccessResult();
        }
        public IDataResult<Url> GetUrlById(int id)
        {
            return new SuccessDataResult<Url>(_urlDal.Get(filter: p => p.Id == id));
        }

        public IDataResult<List<Url>> GetUrlList()
        {
            return new SuccessDataResult<List<Url>>(_urlDal.GetList(null).ToList());
        }

        public IDataResult<List<Url>> GetUrlListByUserId(int userId)
        {
            return new SuccessDataResult<List<Url>>(_urlDal.GetList(p=>p.UserId==userId).ToList());

        }
    }
}
