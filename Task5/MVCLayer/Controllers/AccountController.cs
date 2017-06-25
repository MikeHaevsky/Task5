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

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            //await SetInitialDataAsync();
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

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            //await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO
                {
                    Email = model.Email,
                    Password = model.Password,
                    Address = model.Address,
                    Name = model.Name,
                    Role = "user"
                };
                OperationDetails operationDetails = await UserService.Create(userDto);
                if (operationDetails.Succedeed)
                {
                    UserModel user = new UserModel
                    {
                        Address = model.Address,
                        Email = model.Email,
                        Name = model.Name,
                        Role = userDto.Role
                    };
                    LoginModel login = new LoginModel
                    {
                        Email=model.Email,
                        Password=model.Password
                    };
                    //UserDTO userDto = new UserDTO { Email = model.Email, Password = model.Password };
                    ClaimsIdentity claim = await UserService.Authenticate(userDto);
                    
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
        [Authorize]
        public ActionResult GetAccount()
        {
            string userId = User.Identity.GetUserId();
            UserDTO userDTO=UserService.GetUser(userId);
            Mapper.Initialize(cfg => cfg.CreateMap<UserDTO, UserModel>());
            UserModel user = Mapper.Map<UserDTO, UserModel>(userDTO);
            return View(user);
        }

        public ActionResult GetAccounts()
        {
            ICollection<UserDTO> userDTOs = UserService.GetUsers();
            Mapper.Initialize(cfg => cfg.CreateMap<UserDTO, UserModel>());
            ICollection<UserModel> users = Mapper.Map<ICollection<UserDTO>, ICollection<UserModel>>(userDTOs);
            return View(users.ToList());
        }
        //private async Task SetInitialDataAsync()
        //{
        //    await UserService.SetInitialData(new UserDTO
        //    {
        //        Email = "kocheryga@ya.ru",
        //        UserName = "kocheryga@ya.ru",
        //        Password = "Rocknroll1988",
        //        Name = "Mike H",
        //        Address = "Grodno",
        //        Role = "admin",
        //    }, new List<string> { "user", "admin" });
        //}
    }
}