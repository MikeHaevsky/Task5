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
            try
            {
                EditManagerViewModel model;
                ManagerDTO managerDTO = operationService.GetManager(id.Value);
                if (managerDTO != null)
                {
                    Mapper.Initialize(opt => opt.CreateMap<ManagerDTO, EditManagerViewModel>());
                    model = Mapper.Map<ManagerDTO, EditManagerViewModel>(managerDTO);

                    return View(model);
                }
                throw new ArgumentException("Manager not found.");
            }
            catch(Exception e)
            {
                ViewBag.ErrorInformation=e.Message;
                return View("Error");
            }
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult EditManager(EditManagerViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Mapper.Initialize(opt => opt.CreateMap<EditManagerViewModel, ManagerDTO>());
                    ManagerDTO managerDTO = Mapper.Map<EditManagerViewModel, ManagerDTO>(model);
                    editSalesService.EditManager(managerDTO);

                    return RedirectToAction("Managers", "Home");
                }
                catch(Exception e)
                {
                    ViewBag.ErrorInformation = e.Message;
                    return View("Error");
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult EditClient(int? id)
        {
            try
            {
                EditClientViewModel model;
                ClientDTO clientDTO = operationService.GetClient(id.Value);
                if (clientDTO != null)
                {
                    Mapper.Initialize(opt => opt.CreateMap<ClientDTO, EditClientViewModel>());
                    model = Mapper.Map<ClientDTO, EditClientViewModel>(clientDTO);

                    return View(model);
                }
                throw new ArgumentException("Client not found.");
            }
            catch (Exception e)
            {
                ViewBag.ErrorInformation = e.Message;
                return View("Error");
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditClient(EditClientViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Mapper.Initialize(opt => opt.CreateMap<EditClientViewModel, ClientDTO>());
                    ClientDTO clientDTO = Mapper.Map<EditClientViewModel, ClientDTO>(model);
                    editSalesService.EditClient(clientDTO);

                    return RedirectToAction("Clients", "Home");
                }
                catch (Exception e)
                {
                    ViewBag.ErrorInformation = e.Message;
                    return View("Error");
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult EditProduct(int? id)
        {
            try
            {
                EditProductViewModel model;
                ProductDTO productDTO = operationService.GetProduct(id.Value);
                if (productDTO != null)
                {
                    Mapper.Initialize(opt => opt.CreateMap<ProductDTO, EditProductViewModel>());
                    model = Mapper.Map<ProductDTO, EditProductViewModel>(productDTO);

                    return View(model);
                }
                throw new ArgumentException("Product not found.");
            }
            catch (Exception e)
            {
                ViewBag.ErrorInformation = e.Message;
                return View("Error");
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditProduct(EditProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Mapper.Initialize(opt => opt.CreateMap<EditProductViewModel, ProductDTO>());
                    ProductDTO productDTO = Mapper.Map<EditProductViewModel, ProductDTO>(model);
                    editSalesService.EditProduct(productDTO);

                    return RedirectToAction("Products", "Home");
                }
                catch (Exception e)
                {
                    ViewBag.ErrorInformation = e.Message;
                    return View("Error");
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult EditOperation(int? id)
        {
            try
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
                throw new ArgumentException("Operation not found.");
            }
            catch (Exception e)
            {
                ViewBag.ErrorInformation = e.Message;
                return View("Error");
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditOperation(EditOperationViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Mapper.Initialize(opt => opt.CreateMap<EditOperationViewModel, OperationDTO>());
                    OperationDTO operationDTO = Mapper.Map<EditOperationViewModel, OperationDTO>(model);
                    editSalesService.EditOperation(operationDTO);

                    return RedirectToAction("Operations", "Home");
                }
                catch (Exception e)
                {
                    ViewBag.ErrorInformation = e.Message;
                    return View("Error");
                }
            }
            return View(model);
        }
    }
}