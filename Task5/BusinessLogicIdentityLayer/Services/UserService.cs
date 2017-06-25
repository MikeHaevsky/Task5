using BusinessLogicIdentityLayer.DTO;
using BusinessLogicIdentityLayer.Infrastructure;
using BusinessLogicIdentityLayer.Interfaces;
using DataAccessLayerIdentity.Entities;
using DataAccessLayerIdentity.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicIdentityLayer.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };

                var result = await Database.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

                await Database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                ClientProfile clientProfile = new ClientProfile { Id = user.Id, Email = userDto.Email, Name = userDto.Name, Address=userDto.Address};

                Database.ClientManager.Create(clientProfile);
                await Database.SaveAsync();

                return new OperationDetails(true, "Registration successfull", "");
            }
            else
            {
                return new OperationDetails(false, "User with this login already exist", "Email");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            ApplicationUser user = await Database.UserManager.FindAsync(userDto.Email, userDto.Password);
            if (user != null)
                claim = await Database.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }

        public UserDTO GetUser(string idUser)
        {
            ApplicationUser user = Database.ClientManager.GetUser(idUser);
            
            UserDTO userDTO = new UserDTO
            {
                Name = user.ClientProfile.Name,
                Address = user.ClientProfile.Address,
                Email = user.Email,
                Role = user.Roles.First(x=>x.UserId==user.Id).ToString()
            };
            return userDTO;
        }

        public ICollection<UserDTO> GetUsers()
        {
            IEnumerable<ApplicationUser> users = Database.ClientManager.GetAllUsers().ToList();
            //IEnumerable<ClientProfile> clients = Database.ClientManager.GetAllUsers();
            ICollection<UserDTO> userDTOs = new List<UserDTO>();
            foreach (ApplicationUser user in users)
            {
                UserDTO userDTO = new UserDTO
                {
                    UserName = user.ClientProfile.Name,
                    Address = user.ClientProfile.Address,
                    Email = user.Email,
                    Role = Database.RoleManager.Roles.FirstOrDefault(x=>x.Id==user.Id).ToString()
                };
                userDTOs.Add(userDTO);
            }
            return userDTOs;
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
