using AutoMapper;
using BusinessLogicIdentityLayer.DTO;
using BusinessLogicIdentityLayer.Infrastructure;
using BusinessLogicIdentityLayer.Interfaces;
using BusinessLogicIdentityLayer.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using MVCLayer.Models;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCLayer.Controllers
{
    [Authorize(Roles = "admin")]
    public class ManageController : Controller
    {
        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                SelectList rolesList = new SelectList(UserService.GetRoles());
                if (rolesList != null)
                {
                    CreateUserViewModel model = new CreateUserViewModel { Roles = rolesList };
                    return View(model);
                }
                else
                    throw new ArgumentException("Role list not found.");
            }
            catch (Exception e)
            {
                ViewBag.ErrorInformation = e.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<CreateUserViewModel, UserDTO>());
                    UserDTO userDTO = Mapper.Map<CreateUserViewModel, UserDTO>(model);

                    if (userDTO.Role == null)
                    {
                        userDTO.Role = "user";
                    }

                    OperationDetails operationDetails = await UserService.Create(userDTO);
                    if (operationDetails.Succedeed)
                    {
                        return RedirectToAction("GetAccounts", "Account");
                    }
                    else
                        throw new ArgumentException("Account not created.");
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
        public async Task<ActionResult> Edit(string id)
        {
            try
            {
                UserDTO userDTO = await UserService.GetUser(id);
                if (userDTO != null)
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<UserDTO, EditUserViewModel>()
                        .ForMember(x => x.Roles, opt => opt.Ignore()));
                    EditUserViewModel editModel = Mapper.Map<UserDTO, EditUserViewModel>(userDTO);
                    editModel.Roles = new SelectList(UserService.GetRoles());

                    return View(editModel);
                }
                else
                    throw new ArgumentException("User not found.");
            }
            catch (Exception e)
            {
                ViewBag.ErrorInformation = e.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<EditUserViewModel, UserDTO>());
                    UserDTO userDTO = Mapper.Map<EditUserViewModel, UserDTO>(model);

                    OperationDetails operationDetails = await UserService.Edit(userDTO);
                    if (operationDetails.Succedeed)
                    {
                        return RedirectToAction("GetAccounts", "Account");
                    }
                    else
                        throw new ArgumentException("Account not saved.");
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
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                UserDTO userDTO = await UserService.GetUser(id);
                if (userDTO != null)
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<UserDTO, UserModel>());
                    UserModel editedModel = Mapper.Map<UserDTO, UserModel>(userDTO);

                    return View(editedModel);
                }
                else
                    throw new ArgumentException("User not found.");
            }
            catch (Exception e)
            {
                ViewBag.ErrorInformation = e.Message;
                return View("Error");
            }
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(UserModel model)
        {
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<UserModel, UserDTO>());
                UserDTO userDTO = Mapper.Map<UserModel, UserDTO>(model);

                OperationDetails operationDetails = await UserService.Delete(userDTO);
                if (operationDetails.Succedeed)
                {
                    return RedirectToAction("GetAccounts", "Account");
                }
                else
                    throw new ArgumentException("Client not deleted.");
            }
            catch (Exception e)
            {
                ViewBag.ErrorInformation = e.Message;
                return View("Error");
            }
        }
    }
}