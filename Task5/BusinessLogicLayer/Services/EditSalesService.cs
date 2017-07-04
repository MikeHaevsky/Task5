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

        public void EditManager(ManagerDTO managerDTO)
        {
            try
            {
                Manager manager = Database.Managers.Get(managerDTO.Id);
                if (manager != null)
                {
                    manager.Nickname = managerDTO.Nickname;

                    Database.Managers.Update(manager);
                    Database.SaveAsync();
                }
                else
                    throw new ArgumentException("Manager is not edit.");
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.ToString());
            }
        }

        public void EditClient(ClientDTO clientDTO)
        {
            try
            {
                Client client = Database.Clients.Get(clientDTO.Id);
                if (client != null)
                {
                    client.Nickname = clientDTO.Nickname;

                    Database.Clients.Update(client);
                    Database.SaveAsync();
                }
                else
                    throw new ArgumentException("Client is not edit.");
            }
            catch(Exception e)
            {
                throw new ArgumentException(e.ToString());
            }
        }

        public void EditProduct(ProductDTO productDTO)
        {
            try
            {
                Product product = Database.Products.Get(productDTO.Id);
                if (product != null)
                {
                    product.Name = productDTO.Name;

                    Database.Products.Update(product);
                    Database.SaveAsync();
                }
                else
                    throw new ArgumentException("Product is not edit.");
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.ToString());
            }
        }

        public void EditOperation(OperationDTO operationDTO)
        {
            try
            {
                Client client = Database.Clients.Get(operationDTO.ClientId);
                if (client == null)
                    throw new ArgumentException("Client not found.");

                Manager manager = Database.Managers.Get(operationDTO.ManagerId);
                if (manager == null)
                    throw new ArgumentException("Manager not found.");

                Product product = Database.Products.Get(operationDTO.ProductId);
                if (product == null)
                    throw new ArgumentException("Product not found.");

                Operation operation = Database.Operations.Get(operationDTO.Id);
                if (operation != null)
                {
                    operation.ClientId = operationDTO.ClientId;
                    operation.ManagerId = operationDTO.ManagerId;
                    operation.ProductId = operationDTO.ProductId;
                    operation.Date = operationDTO.Date;
                    operation.Cost = operationDTO.Cost;

                    Database.Operations.Update(operation);
                    Database.SaveAsync();
                }
                else
                    throw new ArgumentException("Operation not found.");
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.ToString());
            }
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
