using Autofac;
using Autofac.Extras.Quartz;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramwork;
using Scheduler.Jobs;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Scheduler
{

    internal class Bootstrap
    {
     

        internal static ContainerBuilder ConfigureContainer(ContainerBuilder cb)
        {
            // configure and register Quartz
            var schedulerConfig = new NameValueCollection {
                {"quartz.threadPool.threadCount", "3"},
                {"quartz.scheduler.threadName", "Scheduler"}
            };

            cb.RegisterModule(new QuartzAutofacFactoryModule
            {
                ConfigurationProvider = c => schedulerConfig
            });

            cb.RegisterModule(new QuartzAutofacJobsModule(typeof(StartJob).Assembly));

            RegisterComponents(cb);
            return cb;
        }

        internal static void RegisterComponents(ContainerBuilder builder)
        {
            // register dependencies         
            builder.RegisterType<UrlService>().As<IUrlService>();
        }
    }
}
