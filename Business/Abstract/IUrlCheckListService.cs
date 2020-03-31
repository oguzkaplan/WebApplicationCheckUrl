using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUrlCheckListService
    {
        IDataResult<List<UrlCheckList>> GetUrlCheckLisByUrlId(int urlId);

        IResult Save(UrlCheckList url);

    }
}
