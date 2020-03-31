using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationCheckUrl.Models;
using WebApplicationCheckUrl.Scheduler.Jobs;

namespace WebApplicationCheckUrl.Scheduler
{
    public class QuartzScheduler : ICustomScheduler
    {
        private JobModel _jobModel;
        string triggerName;
        string jobName;
        string groupName;
        public void StartJob(JobModel jobModel)
        {
            setKeys(jobModel);
            initializeStartJobs(true);
        }

        public void UpdateJob(JobModel jobModel)
        {
            setKeys(jobModel);
            initializeStopJobs();
        }

        public async void DeleteJob(JobModel jobModel)
        {
            setKeys(jobModel);
            IScheduler _scheduler = null;
            _scheduler = await new StdSchedulerFactory().GetScheduler();
            Task<bool> unscheduleJob = _scheduler.UnscheduleJob(new TriggerKey(triggerName, groupName));
            Task<bool> deleteJob = _scheduler.DeleteJob(new JobKey(jobName, groupName));
        }

        public async void initializeStopJobs()
        {
            IScheduler _scheduler = null;
            _scheduler = await new StdSchedulerFactory().GetScheduler();
            Task<bool> unscheduleJob = _scheduler.UnscheduleJob(new TriggerKey(triggerName, groupName));
            Task<bool> deleteJob = _scheduler.DeleteJob(new JobKey(jobName, groupName));
            initializeStartJobs(false);
        }

        public async void initializeStartJobs(bool first)
        {
            IScheduler _scheduler = null;
            _scheduler = await new StdSchedulerFactory().GetScheduler();
            await _scheduler.Start();
            ITrigger trigger = null;
            if (_jobModel.RepeatForever)
            {
                trigger = TriggerBuilder.Create()
            .WithIdentity(triggerName, groupName).StartAt(first ? _jobModel.StartDate.Value : DateTime.Now).WithSimpleSchedule(x => x.RepeatForever().WithIntervalInMinutes(_jobModel.Minutes)).Build();
            }
            else
            {
                trigger = TriggerBuilder.Create()
           .WithIdentity(triggerName, groupName).StartAt(first ? _jobModel.StartDate.Value : DateTime.Now).WithSimpleSchedule(x => x.WithIntervalInMinutes(_jobModel.Minutes)).EndAt(_jobModel.EndDate).Build();

            }
            IJobDetail job = JobBuilder.Create<StartJob>().WithIdentity(jobName, groupName).UsingJobData("urlId", _jobModel.Key).UsingJobData("url", _jobModel.Url).UsingJobData("mailaddress", _jobModel.MailAddress).Build();

            var result = await _scheduler.ScheduleJob(job, trigger);
        }

        void setKeys(JobModel jobModel)
        {
            _jobModel = jobModel;
            triggerName = _jobModel.Key + "_trigger";
            jobName = _jobModel.Key + "_job";
            groupName = "urlCheckGroup";
        }

    }
}
