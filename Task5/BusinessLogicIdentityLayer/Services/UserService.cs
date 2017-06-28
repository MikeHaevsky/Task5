using AutoMapper;
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

        public async Task<OperationDetails> Create(UserDTO userDTO)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userDTO.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDTO.Email, UserName = userDTO.Email };

                Mapper.Initialize(cfg => cfg.CreateMap<UserDTO, ClientProfile>());
                //ClientProfile clientProfile = Mapper.Map<UserDTO, ClientProfile>(userDTO);
                user.ClientProfile = Mapper.Map<UserDTO, ClientProfile>(userDTO);

                var result = await Database.UserManager.CreateAsync(user, userDTO.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

                await Database.UserManager.AddToRoleAsync(user.Id, userDTO.Role);

                return new OperationDetails(true, "Registration successfull", "");
            }
            else
            {
                return new OperationDetails(false, "User with this login already exist", "Email");
            }
        }

        public async Task<OperationDetails> Edit(UserDTO userDTO)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userDTO.Email);
            if (user != null)
            {
                user.Email = userDTO.Email;

                var result = await Database.UserManager.UpdateAsync(user);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

                Mapper.Initialize(cfg => cfg.CreateMap<UserDTO, ClientProfile>());
                ClientProfile clientProfile = Mapper.Map<UserDTO, ClientProfile>(userDTO);
                await Database.ClientManager.Update(clientProfile);

                List<string> roles = GetRoles();
                foreach (string role in roles)
                {
                    await Database.UserManager.RemoveFromRoleAsync(user.Id, role);
                }
                await Database.UserManager.AddToRoleAsync(user.Id, userDTO.Role);

                return new OperationDetails(true, "Update successfull", "");
            }
            else
            {
                return new OperationDetails(false, "User with this login not existed", "Email");
            }
        }

        public async Task<OperationDetails> Delete(UserDTO userDTO)
        {
            ApplicationUser user = await Database.UserManager.FindByIdAsync(userDTO.Id);
            if (user != null)
            {
                await Database.ClientManager.Delete(user.ClientProfile);
                var result = await Database.UserManager.DeleteAsync(user);
                
                await Database.SaveAsync();
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

                return new OperationDetails(true, "Delete successfull", "");
            }
            else
            {
                return new OperationDetails(false, "User with this login not existed", "Email");
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

        public List<string> GetRoles()
        {
            return Database.RoleManager.Roles.Select(x => x.Name).ToList();
        }

        public async Task<IEnumerable<string>> GetCurrentRoles(ApplicationUser user)
        {
            return await Database.UserManager.GetRolesAsync(user.Id);
        }

        public async Task<UserDTO> GetUser(string idUser)
        {
            ApplicationUser user = await Database.UserManager.FindByIdAsync(idUser);
            IEnumerable<string> userRole = await GetCurrentRoles(user);

            Mapper.Initialize(cfg => cfg.CreateMap<ApplicationUser, UserDTO>()
                .ForMember(x => x.Name, opt => opt.MapFrom(item => item.ClientProfile.Name))
                .ForMember(x => x.Address, opt => opt.MapFrom(item => item.ClientProfile.Address))
                .ForMember(x => x.Roles, opt => opt.MapFrom(item => userRole)));

            UserDTO userDTO = Mapper.Map<ApplicationUser, UserDTO>(user);

            return userDTO;
        }

        public async Task<ICollection<UserDTO>> GetUsers()
        {
            IEnumerable<ApplicationUser> users = Database.ClientManager.GetAllUsers().ToList();
            ICollection<UserDTO> userDTOs = new List<UserDTO>();

            foreach (ApplicationUser user in users)
            {
                IEnumerable<string> userRole = await GetCurrentRoles(user);

                Mapper.Initialize(cfg => cfg.CreateMap<ApplicationUser, UserDTO>()
                    .ForMember(x => x.Name, opt => opt.MapFrom(item => item.ClientProfile.Name))
                    .ForMember(x => x.Address, opt => opt.MapFrom(item => item.ClientProfile.Address))
                    .ForMember(x => x.Roles, opt => opt.MapFrom(item => userRole))
                    .ForMember(x => x.Id, opt => opt.MapFrom(item => item.Id)));

                UserDTO userDTO = Mapper.Map<ApplicationUser, UserDTO>(user);

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
