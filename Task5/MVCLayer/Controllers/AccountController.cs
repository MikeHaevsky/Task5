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
    [Authorize]
    public class AccountController : Controller
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

        [AllowAnonymous][HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous][HttpPost][ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO { Email = model.Email, Password = model.Password };
                ClaimsIdentity claim = await UserService.Authenticate(userDto);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Incorrect login or password");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous][HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous][HttpPost][ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<RegisterModel, UserDTO>()
                    .ForMember(x=>x.Id,opt=>opt.Ignore()));
                UserDTO userDTO = Mapper.Map<RegisterModel, UserDTO>(model);
                userDTO.Role = "user";

                OperationDetails operationDetails = await UserService.Create(userDTO);
                if (operationDetails.Succedeed)
                {
                    UserDTO newUserDTO = new UserDTO { Email = model.Email, Password = model.Password };
                    ClaimsIdentity claim = await UserService.Authenticate(newUserDTO);
                    
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
                    //string message = "Congratulations! You successfully registered";
                    //await Login(login);
                    //return RedirectToAction("Login", "Account",login);
                        //View("SuccessRegister",user);
                }
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> GetAccount()
        {
            string userId = User.Identity.GetUserId();
            UserDTO userDTO=await UserService.GetUser(userId);
            Mapper.Initialize(cfg => cfg.CreateMap<UserDTO, UserModel>());
            UserModel user = Mapper.Map<UserDTO, UserModel>(userDTO);

            return View(user);
        }

        public async Task<ActionResult> GetAccounts()
        {
            ICollection<UserDTO> userDTOs = await UserService.GetUsers();
            Mapper.Initialize(cfg => cfg.CreateMap<UserDTO, UserModel>());
            ICollection<UserModel> users = Mapper.Map<ICollection<UserDTO>, ICollection<UserModel>>(userDTOs);
            return View(users.ToList());
        }

        [AllowAnonymous]
        private async Task SetInitialDataAsync()
        {
            await UserService.SetInitialData(new UserDTO
            {
                Email = "kocheryga@ya.ru",
                UserName = "kocheryga@ya.ru",
                Password = "Rocknroll1988",
                Name = "Mike H",
                Address = "Grodno",
                Role = "admin",
            }, new List<string> { "user", "admin" });
        }
    }
}