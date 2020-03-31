using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramwork.Contexts;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramwork
{
    public class EfUrlDal : EfEntityRepositoryBase<Url, CheckUrlContext>, IUrlDal
    {
        
    }
}
