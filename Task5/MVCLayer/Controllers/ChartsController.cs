using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Interfaces;
using MVCLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLayer.Controllers
{
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
            IEnumerable<ManagerDTOSumCost> managerDTOs = chartsService.GetSumCostManager();
            Mapper.Initialize(opt => opt.CreateMap<ManagerDTOSumCost, ManagerSumCostViewModel>());
            var model = Mapper.Map<IEnumerable<ManagerDTOSumCost>, IEnumerable<ManagerSumCostViewModel>>(managerDTOs);

            return Json(model, JsonRequestBehavior.AllowGet);
        }
	}
}