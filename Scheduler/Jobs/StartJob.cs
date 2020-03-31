using Business.Abstract;
using Quartz;
using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Jobs
{
    public class StartJob : IJob
    {
        private readonly IUrlService _urlService;

        public StartJob(IUrlService urlService)
        {
            _urlService = urlService;
        }

        private string _url;

        public Task Execute(IJobExecutionContext context)
        {
            JobKey key = context.JobDetail.Key;
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            _url = dataMap.GetString("url");
            return Task.Run(() => startJob());
        }


        private async void startJob()
        {
            _urlService.GetUrlById(1);
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync(_url);

            if (!response.IsSuccessStatusCode)
            {

            }
        }

    }
}
