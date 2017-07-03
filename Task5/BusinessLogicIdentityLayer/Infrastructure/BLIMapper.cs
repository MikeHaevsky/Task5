using AutoMapper;
using BusinessLogicIdentityLayer.DTO;
using DataAccessLayerIdentity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicIdentityLayer.Infrastructure
{
    public class BLIMapper
    {
        public ClientProfile Mapping(UserDTO userDTO)
        {
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<UserDTO, ClientProfile>());
                return Mapper.Map<UserDTO, ClientProfile>(userDTO);
            }
            catch (Exception e)
            {
                throw new ArgumentException("ERROR MAPPER on mapping UserDTO to CLientProfile." + e.ToString());
            }
        }

        public UserDTO Mapping(ApplicationUser user, IEnumerable<string> userRole)
        {
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<ApplicationUser, UserDTO>()
                .ForMember(x => x.Name, opt => opt.MapFrom(item => item.ClientProfile.Name))
                .ForMember(x => x.Address, opt => opt.MapFrom(item => item.ClientProfile.Address))
                .ForMember(x => x.Roles, opt => opt.MapFrom(item => userRole)));

                return Mapper.Map<ApplicationUser, UserDTO>(user);
            }
            catch (Exception e)
            {
                throw new ArgumentException("ERROR MAPPER on mapping ApplicationUser to UserDTO."+e.InnerException);
            }
        }
    }
}
