using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Infrastructure;
using BusinessLogicLayer.Interfaces;
using Microsoft.Owin.Security;
using MVCLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLayer.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        IOperationService operationService;
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public HomeController(IOperationService service)
        {
            operationService = service;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Clients()
        {
            try
            {
                IEnumerable<ClientDTO> clientDTOs = operationService.GetClients();
                Mapper.Initialize(cfg => cfg.CreateMap<ClientDTO, ClientViewModel>());
                var clients = Mapper.Map<IEnumerable<ClientDTO>, List<ClientViewModel>>(clientDTOs);

                bool auth = AuthenticationManager.User.IsInRole("admin");
                ViewBag.IsAdmin = auth;

                return View(clients);
            }
            catch (Exception e)
            {
                ViewBag.ErrorInformation(e.ToString());
                return View("Error");
            }
        }

        public ActionResult Managers()
        {
            bool auth = AuthenticationManager.User.IsInRole("admin");
            try
            {
                IEnumerable<ManagerDTO> managerDTOs = operationService.GetManagers();
                Mapper.Initialize(cfg => cfg.CreateMap<ManagerDTO, ManagerViewModel>());
                var managers = Mapper.Map<IEnumerable<ManagerDTO>, List<ManagerViewModel>>(managerDTOs);

                ViewBag.IsAdmin = auth;

                return View(managers);
            }
            catch (Exception e)
            {
                if (auth)
                {
                    ViewBag.ErrorInformation(e.ToString());
                    return View("Error");
                }
                else
                    return HttpNotFound();
            }
        }

        public ActionResult Products()
        {
            bool auth = AuthenticationManager.User.IsInRole("admin");
            try
            {
                IEnumerable<ProductDTO> productDTOs = operationService.GetProducts();
                Mapper.Initialize(cfg => cfg.CreateMap<ProductDTO, ProductViewModel>());
                var products = Mapper.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(productDTOs);

                ViewBag.IsAdmin = auth;
                return View(products);
            }
            catch (Exception e)
            {
                if (auth)
                {
                    ViewBag.ErrorInformation(e.ToString());
                    return View("Error");
                }
                else
                    return HttpNotFound();
            }
        }

        [HttpGet]
        public ActionResult Operations(int? manager, int? client, int? product, int? lowCost, int? highCost, DateTime? date)
        {
            bool auth = AuthenticationManager.User.IsInRole("admin");

            try
            {
                IEnumerable<OperationDTO> operationDTOs = operationService.GetOperations();

                if (manager != null && manager != 0)
                {
                    operationDTOs = operationDTOs.Where(p => p.ManagerId == manager);
                }

                if (client != null && client != 0)
                {
                    operationDTOs = operationDTOs.Where(p => p.ClientId == client);
                }

                if (product != null && product != 0)
                {
                    operationDTOs = operationDTOs.Where(p => p.ProductId == product);
                }

                if (lowCost != null && highCost != null && highCost != 0)
                {
                    operationDTOs = operationDTOs.Where(p => p.Cost >= lowCost && p.Cost <= highCost);
                }

                if (date != null)
                {
                    operationDTOs = operationDTOs.Where(p => p.Date == date);
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
                    Products = new SelectList(productDTOs, "Id", "Name"),
                    SomeOperations = operations.ToList()
                };

                ViewBag.IsAdmin = auth;

                return View(opv);
            }
            catch (Exception e)
            {
                if (auth)
                {
                    ViewBag.ErrorInformation(e.ToString());
                    return View("Error");
                }
                else
                    return HttpNotFound();
            }
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}