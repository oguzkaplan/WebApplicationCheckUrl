using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUrlService
    {
        IDataResult<List<Url>> GetUrlList();

        IDataResult<List<Url>> GetUrlListByUserId(int userId);

        IDataResult<Url> GetUrlById(int id);

        IResult Save(Url url);

        IResult Update(Url url);

        IResult Delete(Url url);


    }
}
