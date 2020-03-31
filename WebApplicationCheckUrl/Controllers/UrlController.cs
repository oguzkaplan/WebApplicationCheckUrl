using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplicationCheckUrl.Models;
using WebApplicationCheckUrl.Scheduler;

namespace WebApplicationCheckUrl.Controllers
{
    public class UrlController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private IUrlService _urlService;
        private IUrlCheckListService _urlCheckListService;
        private IUrlSchedulerService _urlSchedulerService;
        private ICustomScheduler _customScheduler;

        public UrlController(ILogger<HomeController> logger, IUrlService urlService, IUrlSchedulerService urlSchedulerService, ICustomScheduler customScheduler, IUrlCheckListService urlCheckListService)
        {
            _logger = logger;
            _urlService = urlService;
            _urlSchedulerService = urlSchedulerService;
            _customScheduler = customScheduler;
            _urlCheckListService = urlCheckListService;
        }
        public IActionResult Index()
        {
            var dataResult = _urlService.GetUrlListByUserId(SessionUserId);
            return View(dataResult.Data);
        }

        [HttpGet]
        public IActionResult UrlForm(int id)
        {




            UrlAndSchedulerModel model = new UrlAndSchedulerModel();
            model.StartDate = DateTime.Now.ToShortDateString();
            if (id != 0)
            {
                var dataResult = _urlService.GetUrlById(id);
                if (dataResult.Success)
                {
                    model.UrlId = id;
                    model.IsActive = Convert.ToBoolean(dataResult.Data.IsActive);
                    model.UrlAddress = dataResult.Data.Address;
                    model.UrlName = dataResult.Data.Name;
                    var urlScheduler = _urlSchedulerService.GetUrlSchedulerListByUrlId(id);
                    if (urlScheduler.Success && (urlScheduler.Data != null))
                    {
                        model.ScheduleId = urlScheduler.Data.Id;
                        model.StartDate = urlScheduler.Data.StartDate.Value.ToShortDateString();
                        if (urlScheduler.Data.RepeatForever == 0)
                        {
                            model.EndDate = urlScheduler.Data.EndDate.Value.ToShortDateString();
                        }
                        model.WithInternalInMinutes = urlScheduler.Data.WithInternalInMinutes;
                        model.Hour = urlScheduler.Data.StartDate.Value.Hour;
                        model.Minute = urlScheduler.Data.StartDate.Value.Minute;
                        model.RepeatForever = Convert.ToBoolean(urlScheduler.Data.RepeatForever);

                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult UrlForm(UrlAndSchedulerModel saveModel)
        {
            if (ModelState.IsValid)
            {
                Url urlSaveModel = new Url();
                urlSaveModel.IsActive = Convert.ToInt32(saveModel.IsActive);
                urlSaveModel.Name = saveModel.UrlName;
                urlSaveModel.Address = saveModel.UrlAddress;
                urlSaveModel.UserId = SessionUserId;
                urlSaveModel.Id = saveModel.UrlId;
                UrlScheduler urlSchedulerSaveModel = new UrlScheduler();
                urlSchedulerSaveModel.Id = saveModel.ScheduleId;
                urlSchedulerSaveModel.IsActive = 1;
                urlSchedulerSaveModel.RepeatForever = 1;
                urlSchedulerSaveModel.WithInternalInMinutes = saveModel.WithInternalInMinutes;
                DateTime convertStartDate = Convert.ToDateTime(saveModel.StartDate);
                DateTime _startDate = new DateTime(convertStartDate.Year, convertStartDate.Month, convertStartDate.Day, saveModel.Hour, saveModel.Minute, 0);
                urlSchedulerSaveModel.StartDate = _startDate;
                urlSchedulerSaveModel.RepeatForever = Convert.ToInt32(saveModel.RepeatForever);
                if (!saveModel.RepeatForever)
                {
                    urlSchedulerSaveModel.EndDate = Convert.ToDateTime(saveModel.EndDate);
                }
                urlSchedulerSaveModel.EndDate = null;
                if (saveModel.UrlId == 0)
                {
                    urlSaveModel.CreateDate = DateTime.Now;
                    _urlService.Save(urlSaveModel);
                    int urlId = urlSaveModel.Id;
                    if (urlId > 0)
                    {
                        urlSchedulerSaveModel.UrlId = urlId;
                        _urlSchedulerService.Save(urlSchedulerSaveModel);
                        if (urlSchedulerSaveModel.Id > 0)
                        {
                            _customScheduler.StartJob(setJob(urlSaveModel, urlSchedulerSaveModel));
                        }
                    }
                }
                else
                {
                    urlSchedulerSaveModel.UrlId = urlSaveModel.Id;
                    _urlSchedulerService.Update(urlSchedulerSaveModel);
                    _urlService.Update(urlSaveModel);
                    _customScheduler.UpdateJob(setJob(urlSaveModel, urlSchedulerSaveModel));

                }
            }



            return RedirectToAction("Index");
        }

        private JobModel setJob(Url urlSaveModel, UrlScheduler urlSchedulerSaveModel)
        {
            JobModel jobModel = new JobModel();
            jobModel.Key = urlSaveModel.Id.ToString();
            jobModel.MailAddress = "oguz.kaplann@gmail.com";
            jobModel.Minutes = urlSchedulerSaveModel.WithInternalInMinutes;
            jobModel.RepeatForever = Convert.ToBoolean(urlSchedulerSaveModel.RepeatForever);
            jobModel.StartDate = urlSchedulerSaveModel.StartDate;
            jobModel.EndDate = urlSchedulerSaveModel.EndDate;
            jobModel.Url = urlSaveModel.Address;
            return jobModel;
        }

        [HttpPost]
        public JsonResult UrlDelete(int urlId)
        {
            bool rtnResult = false;
            var resultData = _urlService.GetUrlById(urlId);
            if (resultData.Success)
            {
                if (resultData.Data.UserId == SessionUserId)
                {
                    _urlService.Delete(resultData.Data);
                    var sResultData = _urlSchedulerService.GetUrlSchedulerListByUrlId(urlId);
                    if (sResultData.Success)
                    {
                        _urlSchedulerService.Delete(sResultData.Data);
                        rtnResult = true;
                        _customScheduler.DeleteJob(setJob(resultData.Data, sResultData.Data));

                    }
                }
            }
            return Json(rtnResult);
        }


        [HttpGet]
        public IActionResult Detail(int id)
        {
            List<UrlCheckList> rtn = null;
            var resultData = _urlCheckListService.GetUrlCheckLisByUrlId(id);
            var resultDataUrl = _urlService.GetUrlById(id);
            if (resultData.Success && resultDataUrl.Success)
            {
                rtn = resultData.Data;
                ViewBag.UrlData = resultDataUrl.Data;
            }
            return View(rtn);
        }

    }
}