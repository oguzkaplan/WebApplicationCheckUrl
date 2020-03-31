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
    public class UrlCheckListService : IUrlCheckListService
    {
        private IUrlCheckListDal _urlCheckListDal;
        public UrlCheckListService(IUrlCheckListDal urlDal)
        {
            _urlCheckListDal = urlDal;
        }
      
        public IResult Save(UrlCheckList urlCheckList)
        {
            _urlCheckListDal.Add(urlCheckList);
            return new SuccessResult();        
        }

        public IDataResult<List<UrlCheckList>> GetUrlCheckLisByUrlId(int urlId)
        {
            return new SuccessDataResult<List<UrlCheckList>>(_urlCheckListDal.GetList(filter: p => p.UrlId == urlId).ToList());

        }
    }
}
