using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Infrastructure;
using BusinessLogicLayer.Interfaces;
using MVCLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLayer.Controllers
{
    public class HomeController : Controller
    {
        IOperationService operationService;

        public HomeController(IOperationService service)
        {
            operationService = service;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Clients()
        {
            IEnumerable<ClientDTO> clientDTOs = operationService.GetClients();
            Mapper.Initialize(cfg => cfg.CreateMap<ClientDTO, ClientView>());
            var clients = Mapper.Map<IEnumerable<ClientDTO>, List<ClientView>>(clientDTOs);
            return View(clients);
        }

        public ActionResult Operation()
        {
            IEnumerable<OperationDTO> operationDTOs = operationService.GetOperations();
            Mapper.Initialize(cfg => cfg.CreateMap<OperationDTO, OperationView>());
            var operations = Mapper.Map<IEnumerable<OperationDTO>, List<OperationView>>(operationDTOs);
            return View(operations);
        }

        [HttpGet]
        public ActionResult Operations(int? manager, int? client, int? product, int? lowCost, int? highCost, DateTime? date)
        {
            IEnumerable<OperationDTO> operationDTOs = operationService.GetOperations();

            if (manager != null && manager != 0)
            {
                try
                {
                    operationDTOs = operationDTOs.Where(p => p.ManagerId == manager);
                }
                catch
                {
                    
                }
            }

            if (client != null && client != 0)
            {
                try
                {
                    operationDTOs = operationDTOs.Where(p => p.ClientId == client);
                }
                catch
                {
                    
                }
            }

            if (product != null && product != 0)
            {
                try
                {
                    operationDTOs = operationDTOs.Where(p => p.ProductId == product);
                }
                catch
                {
                    
                }
            }

            if (lowCost != null || highCost != null)
            {
                try
                {
                    operationDTOs = operationDTOs.Where(p => p.Cost >= lowCost && p.Cost <= highCost);
                }
                catch
                {
                    
                }
            }

            if (date != null)
            {
                try
                {
                    operationDTOs = operationDTOs.Where(p => p.Date == date);
                }
                catch
                {
                    
                }
            }

            Mapper.Initialize(cfg => cfg.CreateMap<OperationDTO, OperationView>());
            IEnumerable<OperationView> operations = Mapper.Map<IEnumerable<OperationDTO>, List<OperationView>>(operationDTOs);

            List<ManagerDTO> managerDTOs = operationService.GetManagers().ToList();
            managerDTOs.Insert(0, new ManagerDTO { Nickname = "All", Id = 0 });

            List<ClientDTO> clientDTOs = operationService.GetClients().ToList();
            clientDTOs.Insert(0, new ClientDTO { Nickname = "All", Id = 0 });

            List<ProductDTO> productDTOs = operationService.GetProducts().ToList();
            productDTOs.Insert(0, new ProductDTO { Name = "All", Id = 0 });

            OperationsView opv = new OperationsView
            {
                Managers = new SelectList(managerDTOs, "Id", "Nickname"),
                Clients = new SelectList(clientDTOs, "Id", "Nickname"),
                Products = new SelectList(productDTOs, "Id", "Name"),
                SomeOperations = operations.ToList()
            };

            opv.MatchesNotFound = (operations == null) ? true : false;

            return View(opv);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Operations()
        {
            return View();
        }
    }
}