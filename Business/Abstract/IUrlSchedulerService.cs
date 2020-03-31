using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUrlSchedulerService
    {
  
        IDataResult<UrlScheduler> GetUrlSchedulerListByUrlId(int urlId);

        IResult Save(UrlScheduler url);

        IResult Update(UrlScheduler url);

        IResult Delete(UrlScheduler url);


    }
}
