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
            Manager manager = Database.Managers.Get(managerDTO.Id);
            if (manager != null)
            {
                manager.Nickname = managerDTO.Nickname;

                Database.Managers.Update(manager);
                Database.Save();
                return new OperationDetails(true, "Update manager success", "");
            }
            else
                return new OperationDetails(false, "Update manager unsuccess", "");
        }

        public OperationDetails EditClient(ClientDTO clientDTO)
        {
            Client client = Database.Clients.Get(clientDTO.Id);
            if (client != null)
            {
                client.Nickname = clientDTO.Nickname;

                Database.Clients.Update(client);
                Database.Save();
                return new OperationDetails(true, "Update client success", "");
            }
            else
                return new OperationDetails(false,"Update client UNsuccess","");
        }

        public OperationDetails EditProduct(ProductDTO productDTO)
        {
            Product product = Database.Products.Get(productDTO.Id);
            if (product != null)
            {
                product.Name=productDTO.Name;

                Database.Products.Update(product);
                Database.Save();
                return new OperationDetails(true, "Update product success", "");
            }
            else
                return new OperationDetails(false,"Product is not exist on DB", "");
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

            Operation operation = Database.Operations.Get(operationDTO.Id);
            if (operation != null)
            {
                operation.ClientId = operationDTO.ClientId;
                operation.ManagerId = operationDTO.ManagerId;
                operation.ProductId = operationDTO.ProductId;
                operation.Date = operationDTO.Date;
                operation.Cost = operationDTO.Cost;

                Database.Operations.Update(operation);
                Database.Save();
                return operationDetails = new OperationDetails(true, "Update operation success", "");
            }
            else
                return operationDetails = new OperationDetails(false, "Update operation UNsuccess", "");
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
