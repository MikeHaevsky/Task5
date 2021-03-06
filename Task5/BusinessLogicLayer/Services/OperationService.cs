﻿using AutoMapper;
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
        BLMapper MapperBL { get; set; }

        public OperationService(IUnitOfWork uow)
        {
            Database = uow;
            MapperBL = new BLMapper();
        }

        public ManagerDTO GetManager(int id)
        {
            try
            {
                Manager manager = Database.Managers.Get(id);
                return MapperBL.Mapping(manager);
            }
            catch (Exception e)
            {
                throw new ArgumentException("GetManager error." + e.ToString());
            }
        }

        public IEnumerable<ManagerDTO> GetManagers()
        {
            try
            {
                IEnumerable<Manager> managers = Database.Managers.GetAll();
                return MapperBL.Mapping(managers);
            }
            catch(Exception e)
            {
                throw new ArgumentException("GetManagers error." + e.ToString());
            }
        }

        public ClientDTO GetClient(int id)
        {
            try
            {
                Client client = Database.Clients.Get(id);
                return MapperBL.Mapping(client);
            }
            catch (Exception e)
            {
                throw new ArgumentException("GetClient error." + e.ToString());
            }
        }

        public IEnumerable<ClientDTO> GetClients()
        {
            try
            {
                IEnumerable<Client> clients = Database.Clients.GetAll();
                return MapperBL.Mapping(clients);
            }
            catch (Exception e)
            {
                throw new ArgumentException("GetClients error." + e.ToString());
            }
        }

        public ProductDTO GetProduct(int id)
        {
            try
            {
                Product product = Database.Products.Get(id);
                return MapperBL.Mapping(product);
            }
            catch (Exception e)
            {
                throw new ArgumentException("GeProduct error." + e.ToString());
            }
        }

        public IEnumerable<ProductDTO> GetProducts()
        {
            try
            {
                IEnumerable<Product> products = Database.Products.GetAll();
                return MapperBL.Mapping(products);
            }
            catch (Exception e)
            {
                throw new ArgumentException("GetProducts error." + e.ToString());
            }
        }

        public OperationDTO GetOperation(int id)
        {
            try
            {
                Operation operation = Database.Operations.Get(id);
                return MapperBL.Mapping(operation);
            }
            catch (Exception e)
            {
                throw new ArgumentException("GetOperation error." + e.ToString());
            }
            
        }

        public IEnumerable<OperationDTO> GetOperations()
        {
            try
            {
                IEnumerable<Operation> operations = Database.Operations.GetAll();
                return MapperBL.Mapping(operations);
            }
            catch (Exception e)
            {
                throw new ArgumentException("GetOperations error." + e.ToString());
            }
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
