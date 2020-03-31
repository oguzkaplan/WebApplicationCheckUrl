using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.DependencyResolvers.Autofac
{
    public class AutofacSchedulerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region Dependency Injection
            builder.RegisterType<QuartzScheduler>().As<ICustomScheduler>();
            #endregion


        }
    }
}
