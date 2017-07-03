using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Infrastructure;
using BusinessLogicLayer.Interfaces;
using MVCLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLayer.Controllers
{
    [Authorize(Roles="admin")]
    public class EditsalesController : Controller
    {
        IOperationService operationService;
        IEditSalesService editSalesService;

        public EditsalesController(IOperationService opService, IEditSalesService edService)
        {
            operationService = opService;
            editSalesService = edService;
        }

        [HttpGet]
        public ActionResult EditManager(int? id)
        {
            if (id != null)
            {
                EditManagerViewModel model;
                ManagerDTO managerDTO = operationService.GetManager(id.Value);
                if (managerDTO != null)
                {
                    Mapper.Initialize(opt => opt.CreateMap<ManagerDTO, EditManagerViewModel>());
                    model = Mapper.Map<ManagerDTO, EditManagerViewModel>(managerDTO);

                    return View(model);
                }
                else
                {
                    model = new EditManagerViewModel { Nickname = "Not found" };
                }
                return View(model);
            }
            return View();
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult EditManager(EditManagerViewModel model)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(opt => opt.CreateMap<EditManagerViewModel, ManagerDTO>());
                ManagerDTO managerDTO = Mapper.Map<EditManagerViewModel, ManagerDTO>(model);

                OperationDetails operationDetails = editSalesService.EditManager(managerDTO);

                if (operationDetails.Succedeed)
                {
                    return RedirectToAction("Managers", "Home");
                }
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult EditClient(int? id)
        {
            if (id == null)
            {
                EditClientViewModel model;
                ClientDTO clientDTO = operationService.GetClient(id.Value);
                if (clientDTO != null)
                {
                    Mapper.Initialize(opt => opt.CreateMap<ClientDTO, EditClientViewModel>());
                    model = Mapper.Map<ClientDTO, EditClientViewModel>(clientDTO);

                    return View(model);
                }
                else
                {
                    model = new EditClientViewModel { Nickname = "Not found" };
                }
                return View(model);
            }
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditClient(EditClientViewModel model)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(opt => opt.CreateMap<EditClientViewModel, ClientDTO>());
                ClientDTO clientDTO = Mapper.Map<EditClientViewModel, ClientDTO>(model);

                OperationDetails operationDetails = editSalesService.EditClient(clientDTO);

                if (operationDetails.Succedeed)
                {
                    return RedirectToAction("Clients", "Home");
                }
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult EditProduct(int? id)
        {
            if (id != null)
            {
                EditProductViewModel model;
                ProductDTO productDTO = operationService.GetProduct(id.Value);
                if (productDTO != null)
                {
                    Mapper.Initialize(opt => opt.CreateMap<ProductDTO, EditProductViewModel>());
                    model = Mapper.Map<ProductDTO, EditProductViewModel>(productDTO);

                    return View(model);
                }
                else
                {
                    model = new EditProductViewModel { Name = "Not found" };
                }
                return View(model);
            }
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditProduct(EditProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(opt => opt.CreateMap<EditProductViewModel, ProductDTO>());
                ProductDTO productDTO = Mapper.Map<EditProductViewModel, ProductDTO>(model);

                OperationDetails operationDetails = editSalesService.EditProduct(productDTO);

                if (operationDetails.Succedeed)
                {
                    return RedirectToAction("Products", "Home");
                }
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult EditOperation(int? id)
        {
            if (id != null)
            {
                EditOperationViewModel model;
                OperationDTO operationDTO = operationService.GetOperation(id.Value);
                if (operationDTO != null)
                {
                    Mapper.Initialize(opt => opt.CreateMap<OperationDTO, EditOperationViewModel>());
                    model = Mapper.Map<OperationDTO, EditOperationViewModel>(operationDTO);

                    List<ManagerDTO> managerDTOs = operationService.GetManagers().ToList();
                    model.ManagerList = new SelectList(managerDTOs,"Id","Nickname");

                    List<ClientDTO> clientDTOs = operationService.GetClients().ToList();
                    model.ClientList = new SelectList(clientDTOs, "Id", "Nickname");

                    List<ProductDTO> productDTOs = operationService.GetProducts().ToList();
                    model.ProductList = new SelectList(productDTOs, "Id", "Name");

                    return View(model);
                }
                else
                {
                    model = new EditOperationViewModel { NotFound = true };
                }
                return View(model);
            }
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditOperation(EditOperationViewModel model)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(opt => opt.CreateMap<EditOperationViewModel, OperationDTO>());
                OperationDTO operationDTO = Mapper.Map<EditOperationViewModel, OperationDTO>(model);

                OperationDetails operationDetails = editSalesService.EditOperation(operationDTO);

                if (operationDetails.Succedeed)
                {
                    return RedirectToAction("Operations", "Home");
                }
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }
	}
}