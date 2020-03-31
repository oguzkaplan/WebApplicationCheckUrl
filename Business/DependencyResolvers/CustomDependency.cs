using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers
{
    public class CustomDependency
    {
        IUrlCheckListService _urlService;
        public CustomDependency(IUrlCheckListService urlService)
        {
            _urlService = urlService;
        }
        public void UrlCheckListSave(UrlCheckList saveModel)
        {
            _urlService.Save(saveModel);
        }

    }


}
