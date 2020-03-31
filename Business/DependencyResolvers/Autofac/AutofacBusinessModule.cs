using Autofac;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region Dependency Injection
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();
            builder.RegisterType<UrlService>().As<IUrlService>();
            builder.RegisterType<EfUrlDal>().As<IUrlDal>();
            builder.RegisterType<EfUrlSchedulerDal>().As<IUrlSchedulerDal>();
            builder.RegisterType<UrlSchedulerService>().As<IUrlSchedulerService>();
            builder.RegisterType<UrlCheckListService>().As<IUrlCheckListService>();
            builder.RegisterType<EfUrlCheckListDal>().As<IUrlCheckListDal>();
            #endregion


        }
    }
}
