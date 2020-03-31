using Autofac;
using Autofac.Extras.Quartz;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramwork;
using Scheduler.Jobs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.DependencyResolvers.Autofac
{
    public class AutofacServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region Dependency Injection
          

            // 1) Register IScheduler
            builder.RegisterModule(new QuartzAutofacFactoryModule());
            // 2) Register jobs
            builder.RegisterModule(new QuartzAutofacJobsModule(typeof(StartJob).Assembly));


            builder.RegisterType<QuartzScheduler>().As<ICustomScheduler>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();
            builder.RegisterType<UrlService>().As<IUrlService>();
            #endregion


        }
    }
}
