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
    public class UrlSchedulerService : IUrlSchedulerService
    {
        private IUrlSchedulerDal _urlSchedulerDal;
        public UrlSchedulerService(IUrlSchedulerDal urlSchedulerDal)
        {
            _urlSchedulerDal = urlSchedulerDal;
        }
        public IResult Delete(UrlScheduler urlScheduler)
        {
            _urlSchedulerDal.Delete(urlScheduler);
            return new SuccessResult();
        }
        public IResult Save(UrlScheduler urlScheduler)
        {
            _urlSchedulerDal.Add(urlScheduler);
            return new SuccessResult();
        }

        public IResult Update(UrlScheduler urlScheduler)
        {
            _urlSchedulerDal.Update(urlScheduler);
            return new SuccessResult();
        }

        public IDataResult<UrlScheduler> GetUrlSchedulerListByUrlId(int urlId)
        {
            return new SuccessDataResult<UrlScheduler>(_urlSchedulerDal.GetList(p => p.UrlId == urlId).ToList().FirstOrDefault());

        }
    }
}
