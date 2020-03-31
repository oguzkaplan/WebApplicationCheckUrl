using Autofac;
using Quartz;
using Quartz.Impl;
using Scheduler.Jobs;
using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Scheduler
{
    public class QuartzScheduler : ICustomScheduler
    {

        private JobModel _jobModel;
        public void StartJob(JobModel jobModel)
        {
            _jobModel = jobModel;
            initializeJobs();
        }



        IScheduler _scheduler = null;
        public async void initializeJobs()
        {
             IContainer container = null;


            container = Bootstrap.ConfigureContainer(new ContainerBuilder()).Build();

            var job = JobBuilder.Create<StartJob>().WithIdentity("Heartbeat", "Maintenance").Build();
            var trigger = TriggerBuilder.Create()
                .WithIdentity("Heartbeat", "Maintenance")
                .StartNow()
                .WithSimpleSchedule(x => x.RepeatForever().WithIntervalInSeconds(5)).Build();
            var cts = new CancellationTokenSource();

            var scheduler = container.Resolve<IScheduler>();
            await scheduler.ScheduleJob(job, trigger, cts.Token).ConfigureAwait(true);

            await scheduler.Start().ConfigureAwait(true);


            // IContainer container = null;

            // container = Bootstrap.ConfigureContainer(new ContainerBuilder()).Build();


            // //  _scheduler = await new StdSchedulerFactory().GetScheduler();
            // _scheduler = container.Resolve<IScheduler>();

            // await _scheduler.Start();

            // ITrigger trigger = null;
            // string triggerName = _jobModel.Key + "_trigger";
            // string jobName = _jobModel.Key + "_job";
            // if (_jobModel.RepeatForever)
            // {
            //     trigger = TriggerBuilder.Create()
            // .WithIdentity(triggerName, "group1").StartAt(_jobModel.StartDate.Value).WithSimpleSchedule(x => x.RepeatForever().WithIntervalInMinutes(_jobModel.Minutes)).Build();
            // }
            // else
            // {
            //     trigger = TriggerBuilder.Create()
            //.WithIdentity(triggerName, "group1").StartAt(_jobModel.StartDate.Value).WithSimpleSchedule(x => x.WithIntervalInMinutes(_jobModel.Minutes)).EndAt(_jobModel.EndDate).Build();

            // }
            // IJobDetail job = JobBuilder.Create<StartJob>().WithIdentity(jobName, "group1").UsingJobData("url", _jobModel.Url).UsingJobData("mailaddress", _jobModel.MailAddress).Build();

            // var result = await _scheduler.ScheduleJob(job, trigger);


        }

    }
}
