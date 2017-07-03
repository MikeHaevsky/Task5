using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Infrastructure;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class OperationService : IOperationService
    {
        IUnitOfWork Database { get; set; }

        public OperationService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public ManagerDTO GetManager(int id)
        {
            Manager manager = Database.Managers.Get(id);
            ManagerDTO managerDTO;

            if (manager != null)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<Manager, ManagerDTO>());
                managerDTO = Mapper.Map<Manager, ManagerDTO>(manager);
            }
            else
                managerDTO=new ManagerDTO();

            return managerDTO;
        }

        public IEnumerable<ManagerDTO> GetManagers()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Manager, ManagerDTO>());
            return Mapper.Map<IEnumerable<Manager>, List<ManagerDTO>>(Database.Managers.GetAll());
        }

        public ClientDTO GetClient(int id)
        {
            Client client = Database.Clients.Get(id);
            ClientDTO clientDTO;

            if (client != null)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<Client, ClientDTO>());
                clientDTO = Mapper.Map<Client, ClientDTO>(client);
            }
            else
                clientDTO = new ClientDTO();

            return clientDTO;
        }

        public IEnumerable<ClientDTO> GetClients()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Client, ClientDTO>());
            return Mapper.Map<IEnumerable<Client>, List<ClientDTO>>(Database.Clients.GetAll());
        }

        public ProductDTO GetProduct(int id)
        {
            Product product = Database.Products.Get(id);
            ProductDTO productDTO;

            if (product != null)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<Product, ProductDTO>());
                productDTO = Mapper.Map<Product, ProductDTO>(product);
            }
            else
                productDTO = new ProductDTO();

            return productDTO;
        }

        public IEnumerable<ProductDTO> GetProducts()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Product, ProductDTO>());
            return Mapper.Map<IEnumerable<Product>, List<ProductDTO>>(Database.Products.GetAll());
        }

        public OperationDTO GetOperation(int id)
        {
            Operation operation = Database.Operations.Get(id);
            OperationDTO operationDTO;

            if (operation != null)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<Operation, OperationDTO>()
                .ForMember(x => x.ClientNickname, opt => opt.MapFrom(item => item.Client.Nickname))
                .ForMember(x => x.ManagerNickname, opt => opt.MapFrom(item => item.Manager.Nickname))
                .ForMember(x => x.ProductName, opt => opt.MapFrom(item => item.Product.Name))
                );
                operationDTO = Mapper.Map<Operation, OperationDTO>(operation);
            }
            else
                operationDTO = new OperationDTO();

            return operationDTO;
        }

        public IEnumerable<OperationDTO> GetOperations()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Operation, OperationDTO>()
                .ForMember(x => x.ClientNickname, opt => opt.MapFrom(item => item.Client.Nickname))
                .ForMember(x => x.ManagerNickname, opt => opt.MapFrom(item => item.Manager.Nickname))
                .ForMember(x => x.ProductName, opt => opt.MapFrom(item => item.Product.Name))
                );
            return Mapper.Map<IEnumerable<Operation>, List<OperationDTO>>(Database.Operations.GetAll());
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
