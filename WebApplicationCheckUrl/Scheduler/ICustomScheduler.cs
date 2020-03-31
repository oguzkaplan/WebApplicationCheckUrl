using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationCheckUrl.Models;

namespace WebApplicationCheckUrl.Scheduler
{
    public interface ICustomScheduler
    {
        void StartJob(JobModel jobModel);
        void UpdateJob(JobModel jobModel);
        void DeleteJob(JobModel jobModel);
    }
}
