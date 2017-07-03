using AutoMapper;
using BusinessLogicLayer.DTO;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class BLMapper
    {
        public ManagerDTO Mapping(Manager manager)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Manager, ManagerDTO>());
            return Mapper.Map<Manager, ManagerDTO>(manager);
        }

        public IEnumerable<ManagerDTO> Mapping(IEnumerable<Manager> managers)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Manager, ManagerDTO>());
            return Mapper.Map<IEnumerable<Manager>,IEnumerable<ManagerDTO>>(managers);
        }

        public IEnumerable<ManagerDTOSumCost> MappingGraph(IEnumerable<Manager> managers)
        {
            Mapper.Initialize(opt => opt.CreateMap<Manager, ManagerDTOSumCost>());
            return Mapper.Map<IEnumerable<Manager>, IEnumerable<ManagerDTOSumCost>>(managers);
        }

        public ClientDTO Mapping(Client client)
        {
            Mapper.Initialize(opt => opt.CreateMap<Client, ClientDTO>());
            return Mapper.Map<Client, ClientDTO>(client);
        }

        public IEnumerable<ClientDTO> Mapping(IEnumerable<Client> clients)
        {
            try
            {
                Mapper.Initialize(opt => opt.CreateMap<Client, ClientDTO>());
                return Mapper.Map<IEnumerable<Client>, IEnumerable<ClientDTO>>(clients);
            }
            catch (Exception e)
            {
                throw new ArgumentException (e.ToString());
            }
        }

        public ProductDTO Mapping(Product product)
        {
            Mapper.Initialize(opt => opt.CreateMap<Product, ProductDTO>());
            return Mapper.Map<Product, ProductDTO>(product);
        }

        public IEnumerable<ProductDTO> Mapping(IEnumerable<Product> product)
        {
            Mapper.Initialize(opt => opt.CreateMap<Product, ProductDTO>());
            return Mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(product);
        }

        public OperationDTO Mapping(Operation operation)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Operation, OperationDTO>()
                    .ForMember(x => x.ClientNickname, opt => opt.MapFrom(item => item.Client.Nickname))
                    .ForMember(x => x.ManagerNickname, opt => opt.MapFrom(item => item.Manager.Nickname))
                    .ForMember(x => x.ProductName, opt => opt.MapFrom(item => item.Product.Name))
                    );
            return Mapper.Map<Operation, OperationDTO>(operation);
        }

        public IEnumerable<OperationDTO> Mapping(IEnumerable<Operation> operations)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Operation, OperationDTO>()
                    .ForMember(x => x.ClientNickname, opt => opt.MapFrom(item => item.Client.Nickname))
                    .ForMember(x => x.ManagerNickname, opt => opt.MapFrom(item => item.Manager.Nickname))
                    .ForMember(x => x.ProductName, opt => opt.MapFrom(item => item.Product.Name))
                    );
            return Mapper.Map <IEnumerable<Operation>, IEnumerable<OperationDTO>>(operations);
        }
    }
}
