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
            //managerDTOs.Add(new ManagerDTOSumCost { Nickname = "0", SumCost = 0 });
            //List<ManagerDTOSumCost> masd = managerDTOs.ToList();
            //List<ManagerSumCostViewModel> model = new List<ManagerSumCostViewModel> {
            //    new ManagerSumCostViewModel { Nickname = "0", SumCost = 0 } };

            List<ManagerSumCostViewModel> model = new List<ManagerSumCostViewModel> {
            new ManagerSumCostViewModel{ Nickname="sssssss", SumCost=2000000}};

            Mapper.Initialize(opt => opt.CreateMap<ManagerDTOSumCost, ManagerSumCostViewModel>());
            foreach (ManagerDTOSumCost manager in managerDTOs)
            {
                model.Add(Mapper.Map<ManagerDTOSumCost, ManagerSumCostViewModel>(manager));
            }
            
            //var model = Mapper.Map<IEnumerable<ManagerDTOSumCost>, List<ManagerSumCostViewModel>>(managerDTOs);
            //model.Add(new ManagerSumCostViewModel { Nickname = "0", SumCost = 0 });

            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}