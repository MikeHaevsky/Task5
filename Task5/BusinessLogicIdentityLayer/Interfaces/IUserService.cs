using BusinessLogicIdentityLayer.DTO;
using BusinessLogicIdentityLayer.Infrastructure;
using DataAccessLayerIdentity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicIdentityLayer.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserDTO userDTO);
        Task<OperationDetails> Edit(UserDTO userDTO);
        Task<OperationDetails> Delete(UserDTO userDTO);
        List<string> GetRoles();
        Task<IEnumerable<string>> GetCurrentRoles(ApplicationUser user);
        Task <UserDTO> GetUser(string idUser);
        Task<ICollection<UserDTO>> GetUsers();
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto, List<string> roles);
    } 
}
