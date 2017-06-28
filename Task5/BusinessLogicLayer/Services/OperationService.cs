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

        public void MakeOperation(OperationDTO operationDTO)
        {
            Client client = Database.Clients.Get(operationDTO.ClientId);
            if (client == null)
                throw new ValidationException("The client is not found","");

            Manager manager = Database.Managers.Get(operationDTO.ManagerId);
            if (manager == null)
                throw new ValidationException("The manager is not found", "");

            Product product=Database.Products.Get(operationDTO.ProductId);
            if (product == null)
                throw new ValidationException("The product is not found", "");

            Mapper.Initialize(cfg => cfg.CreateMap<OperationDTO, Operation>()
                .ForMember(x=>x.Date,opt=>opt.MapFrom(item=>DateTime.Now)));

            Operation operation = Mapper.Map<OperationDTO, Operation>(operationDTO);
            //Operation operation = new Operation
            //{
            //    Date = DateTime.Now,
            //    ClientId = operationDTO.ClientId,
            //    ManagerId = operationDTO.ManagerId,
            //    ProductId = operationDTO.ProductId,
            //    Cost = operationDTO.Cost
            //};
            Database.Operations.Create(operation);
            Database.Save();
        }

        public DTO.ManagerDTO GetManager(int? id)
        {
            if (id == null)
                throw new ValidationException("The id-number of manager is not found", "");
            var manager = Database.Managers.Get(id.Value);
            if (manager == null)
                throw new ValidationException("The client is not found in Database", "");
            Mapper.Initialize(cfg => cfg.CreateMap<Manager, ManagerDTO>());
            return Mapper.Map<Manager, ManagerDTO>(manager);
        }

        public IEnumerable<ManagerDTO> GetManagers()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Manager, ManagerDTO>());
            return Mapper.Map<IEnumerable<Manager>, List<ManagerDTO>>(Database.Managers.GetAll());
        }

        public IEnumerable<ManagerDTO> GetManagers(int id)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Manager, ManagerDTO>());
            return Mapper.Map<IEnumerable<Manager>, List<ManagerDTO>>(Database.Managers.GetAll());
        }

        public DTO.ClientDTO GetClient(int? id)
        {
            if (id == null)
                throw new ValidationException("The id-number of client is not found", "");
            var client = Database.Clients.Get(id.Value);
            if (client == null)
                throw new ValidationException("The manager is not found in Database", "");
            Mapper.Initialize(cfg => cfg.CreateMap<Client, ClientDTO>());
            return Mapper.Map<Client, ClientDTO>(client);
        }

        public IEnumerable<ClientDTO> GetClients()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Client, ClientDTO>());
            return Mapper.Map<IEnumerable<Client>, List<ClientDTO>>(Database.Clients.GetAll());
        }

        public DTO.ProductDTO GetProduct(int? id)
        {
            if (id == null)
                throw new ValidationException("The id-number of product is not found", "");
            var product = Database.Products.Get(id.Value);
            if (product == null)
                throw new ValidationException("The product is not found in Database", "");
            Mapper.Initialize(cfg => cfg.CreateMap<Product, ProductDTO>());
            return Mapper.Map<Product, ProductDTO>(product);
        }

        public IEnumerable<ProductDTO> GetProducts()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Product, ProductDTO>());
            return Mapper.Map<IEnumerable<Product>, List<ProductDTO>>(Database.Products.GetAll());
        }

        public DTO.OperationDTO GetOperation(int? id)
        {
            if (id == null)
                throw new ValidationException("The id-number of operation is not found", "");
            var operation = Database.Operations.Get(id.Value);
            if (operation == null)
                throw new ValidationException("The operation is not found in Database", "");
            Mapper.Initialize(cfg => cfg.CreateMap<Operation, OperationDTO>()
                .ForMember(x=>x.ClientNickname, opt => opt.MapFrom(item => item.Client.Nickname))
                .ForMember(x=>x.ManagerNickname, opt => opt.MapFrom(item => item.Manager.Nickname))
                .ForMember(x=>x.ProductName, opt => opt.MapFrom(item => item.Product.Name))
                );
            return Mapper.Map<Operation, OperationDTO>(operation);
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
