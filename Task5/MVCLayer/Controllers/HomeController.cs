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

        [Authorize]
        public ActionResult Clients()
        {
            IEnumerable<ClientDTO> clientDTOs = operationService.GetClients();
            Mapper.Initialize(cfg => cfg.CreateMap<ClientDTO, ClientViewModel>());
            var clients = Mapper.Map<IEnumerable<ClientDTO>, List<ClientViewModel>>(clientDTOs);
            return View(clients);
        }

        [Authorize]
        public ActionResult Managers()
        {
            IEnumerable<ManagerDTO> managerDTOs = operationService.GetManagers();
            Mapper.Initialize(cfg => cfg.CreateMap<ManagerDTO, ManagerViewModel>());
            var managers = Mapper.Map<IEnumerable<ManagerDTO>, List<ManagerViewModel>>(managerDTOs);
            return View(managers);
        }

        [Authorize]
        public ActionResult Products()
        {
            IEnumerable<ProductDTO> productDTOs = operationService.GetProducts();
            Mapper.Initialize(cfg => cfg.CreateMap<ProductDTO, ProductViewModel>());
            var products = Mapper.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(productDTOs);
            return View(products);
        }

        [Authorize]
        public ActionResult Operation()
        {
            IEnumerable<OperationDTO> operationDTOs = operationService.GetOperations();
            Mapper.Initialize(cfg => cfg.CreateMap<OperationDTO, OperationViewModel>());
            var operations = Mapper.Map<IEnumerable<OperationDTO>, List<OperationViewModel>>(operationDTOs);
            return View(operations);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Operations(int? manager, int? client, int? product, int? lowCost, int? highCost, DateTime? date)
        {
            IEnumerable<OperationDTO> operationDTOs = operationService.GetOperations();

            if (manager != null && manager != 0)
            {
                try
                { operationDTOs = operationDTOs.Where(p => p.ManagerId == manager);}
                catch
                { ViewBag.Message= ViewBag.Message+"Manager with id="+manager.ToString()+" not found";}
            }

            if (client != null && client != 0)
            {
                try
                { operationDTOs = operationDTOs.Where(p => p.ClientId == client);}
                catch
                { ViewBag.Message = ViewBag.Message + "|Client with id=" + client.ToString() + " not found";}
            }

            if (product != null && product != 0)
            {
                try
                { operationDTOs = operationDTOs.Where(p => p.ProductId == product);}
                catch
                { ViewBag.Message = ViewBag.Message + "|Product with id=" + product.ToString() + " not found";}
            }

            if (lowCost != null && highCost!=null && highCost!=0)
            {
                try
                { operationDTOs = operationDTOs.Where(p => p.Cost >= lowCost && p.Cost <= highCost);}
                catch
                { ViewBag.Message = ViewBag.Message + "|Operations not found from current cost interval " + lowCost.ToString() + "/" + highCost.ToString();}
            }

            if (date != null)
            {
                try
                { operationDTOs = operationDTOs.Where(p => p.Date == date);}
                catch
                { ViewBag.Message = ViewBag.Message + "|Operations at " + date.ToString() + " not found";}
            }
            if (operationDTOs.Count() == 0)
                ViewBag.Message = ViewBag.Message + "Operations not found";

            Mapper.Initialize(cfg => cfg.CreateMap<OperationDTO, OperationViewModel>());
            IEnumerable<OperationViewModel> operations = Mapper.Map<IEnumerable<OperationDTO>, List<OperationViewModel>>(operationDTOs);

            List<ManagerDTO> managerDTOs = operationService.GetManagers().ToList();
            managerDTOs.Insert(0, new ManagerDTO { Nickname = "All", Id = 0 });

            List<ClientDTO> clientDTOs = operationService.GetClients().ToList();
            clientDTOs.Insert(0, new ClientDTO { Nickname = "All", Id = 0 });

            List<ProductDTO> productDTOs = operationService.GetProducts().ToList();
            productDTOs.Insert(0, new ProductDTO { Name = "All", Id = 0 });

            OperationsViewModel opv = new OperationsViewModel
            {
                Managers = new SelectList(managerDTOs, "Id", "Nickname"),
                Clients = new SelectList(clientDTOs, "Id", "Nickname"),
                Products = new SelectList(productDTOs, "Id", "Nickname"),
                SomeOperations = operations.ToList()
            };

            return View(opv);
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