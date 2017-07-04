using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Interfaces;
using MVCLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVCLayer.Controllers
{
    [Authorize]
    public class ChartsController : Controller
    {
        IChartsService chartsService;

        public ChartsController(IChartsService service)
        {
            chartsService = service;
        }

        [HttpGet]
        public ActionResult ChartManagerSumCost()
        {
            return View();
        }

        public JsonResult GetChartManagerSumCost()
        {
            List<ManagerDTOSumCost> managerDTOs = chartsService.GetSumCostManager().ToList();

            List<ManagerSumCostViewModel> model = new List<ManagerSumCostViewModel>();

            Mapper.Initialize(opt => opt.CreateMap<ManagerDTOSumCost, ManagerSumCostViewModel>());
            foreach (ManagerDTOSumCost manager in managerDTOs)
            {
                model.Add(Mapper.Map<ManagerDTOSumCost, ManagerSumCostViewModel>(manager));
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}