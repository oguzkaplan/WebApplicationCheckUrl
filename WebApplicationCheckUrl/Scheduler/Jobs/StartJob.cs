using Autofac;
using Business.Concrete;
using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplicationCheckUrl.Controllers;
using DataAccess.Concrete.EntityFramwork;
using DataAccess.Abstract;
using Business.DependencyResolvers;
using Entities.Concrete;

namespace WebApplicationCheckUrl.Scheduler.Jobs
{
    public class StartJob : IJob
    {

        private string _url;
        private string _mailaddress;
        private string _urlId;
        public Task Execute(IJobExecutionContext context)
        {
            JobKey key = context.JobDetail.Key;
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            _url = dataMap.GetString("url");
            _urlId = dataMap.GetString("urlId");
            _mailaddress = dataMap.GetString("mailaddress");
            return Task.Run(() => startJob());
        }

        private void startJob()
        {

            HttpClient httpClient = new HttpClient();
            Uri uriResult;
            if (!Uri.TryCreate("_url", UriKind.Absolute, out uriResult))
            {
                _url = "http://" + _url;
            }
            UrlCheckList urlCheckList = new UrlCheckList();

            try
            {
                Task<HttpResponseMessage> response = httpClient.GetAsync(_url);
                urlCheckList.Id = 0;
                urlCheckList.CreateDate = DateTime.Now;
                urlCheckList.UrlId = Convert.ToInt32(_urlId);
                urlCheckList.StatusCode = response.Result.StatusCode.ToString();
                if (!response.Result.IsSuccessStatusCode)
                {
                    SendMail();
                }
            }
            catch (Exception)
            {
                //Custom Error Handlen Yazılacak
                urlCheckList.StatusCode = "Domain Error";

            }
            CustomDependency c = new CustomDependency(new UrlCheckListService(new EfUrlCheckListDal()));
            c.UrlCheckListSave(urlCheckList);
        }

        private void SendMail()
        {
            //
        }
    }
}
