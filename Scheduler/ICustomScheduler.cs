using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler
{
   public interface ICustomScheduler
    {
        void StartJob(JobModel jobModel);  
        
    }
}
