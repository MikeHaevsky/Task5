using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Infrastructure;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class EditSalesService:IEditSalesService
    {
        IUnitOfWork Database { get; set; }

        public EditSalesService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public OperationDetails EditManager(ManagerDTO managerDTO)
        {
            Manager managerDB = Database.Managers.Get(managerDTO.Id);
            if (managerDB == null)
            return new OperationDetails(false, "Manager is not exist on DB", "");
            else
            {
                Mapper.Initialize(opt => opt.CreateMap<ManagerDTO, Manager>());
                Manager manager = Mapper.Map<ManagerDTO, Manager>(managerDTO);
                manager.Operations = managerDB.Operations;

                Database.Managers.Update(manager);
                Database.Save();
                return new OperationDetails(true, "Update manager success", "");
            }
        }

        public OperationDetails EditClient(ClientDTO clientDTO)
        {
            Client client = Database.Clients.Get(clientDTO.Id);
            if (client == null)
                return new OperationDetails(false,"Client is not exist on DB", "");
            else
            {
                Mapper.Initialize(opt => opt.CreateMap<ClientDTO, Client>());
                client = Mapper.Map<ClientDTO, Client>(clientDTO);

                Database.Clients.Update(client);
                Database.Save();
                return new OperationDetails(true, "Update client success", "");
            }
        }

        public OperationDetails EditProduct(ProductDTO productDTO)
        {
            Product product = Database.Products.Get(productDTO.Id);
            if (product == null)
                return new OperationDetails(false,"Product is not exist on DB", "");
            else
            {
                Mapper.Initialize(opt => opt.CreateMap<ProductDTO, Product>());
                product = Mapper.Map<ProductDTO, Product>(productDTO);

                Database.Products.Update(product);
                Database.Save();
                return new OperationDetails(true, "Update product success", "");
            }
        }

        public OperationDetails EditOperation(OperationDTO operationDTO)
        {
            OperationDetails operationDetails;
            Client client = Database.Clients.Get(operationDTO.ClientId);
            if (client == null)
                return operationDetails = new OperationDetails(false,"/The client is not found/","");

            Manager manager = Database.Managers.Get(operationDTO.ManagerId);
            if (manager == null)
                return operationDetails = new OperationDetails(false, "/The manager is not found/", "");

            Product product=Database.Products.Get(operationDTO.ProductId);
            if (product == null)
                return operationDetails = new OperationDetails(false, "/The product is not found/", "");

            Mapper.Initialize(opt => opt.CreateMap<OperationDTO, Operation>());
            Operation operation = Mapper.Map<OperationDTO, Operation>(operationDTO);

            Database.Operations.Update(operation);
            Database.Save();
            return operationDetails = new OperationDetails(true, "Update operation success", "");
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
