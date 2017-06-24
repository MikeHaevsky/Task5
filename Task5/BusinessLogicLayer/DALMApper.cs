using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class DALMApper
    {
        #region BL to DAL

        internal DataAccessLayer.Entities.Client MappingToDAL(DTO.ClientDTO client)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DTO.ClientDTO,DataAccessLayer.Entities.Client>());
            return Mapper.Map<DTO.ClientDTO,DataAccessLayer.Entities.Client>(client);
        }

        internal DataAccessLayer.Entities.Manager MappingToDAL(DTO.ManagerDTO manager)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DTO.ManagerDTO, DataAccessLayer.Entities.Manager>());
            return Mapper.Map<DTO.ManagerDTO, DataAccessLayer.Entities.Manager>(manager);
        }

        internal DataAccessLayer.Entities.Product MappingToDAL(DTO.ProductDTO product)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DTO.ProductDTO, DataAccessLayer.Entities.Product>());
            return Mapper.Map<DTO.ProductDTO, DataAccessLayer.Entities.Product>(product);
        }

        internal DataAccessLayer.Entities.Operation MappingToDAL(DTO.OperationDTO operation)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DTO.OperationDTO, DataAccessLayer.Entities.Operation>());
            return Mapper.Map<DTO.OperationDTO, DataAccessLayer.Entities.Operation>(operation);
        }

        #endregion


        #region DAL to BL

        internal DTO.ClientDTO MappingToBL(DataAccessLayer.Entities.Client client)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DataAccessLayer.Entities.Client, DTO.ClientDTO>());
            return Mapper.Map<DataAccessLayer.Entities.Client, DTO.ClientDTO>(client);
        }

        internal DTO.ManagerDTO MappingToBL(DataAccessLayer.Entities.Manager manager)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DataAccessLayer.Entities.Manager, DTO.ManagerDTO>());
            return Mapper.Map<DataAccessLayer.Entities.Manager, DTO.ManagerDTO>(manager);
        }

        internal DTO.ProductDTO MappingToBL(DataAccessLayer.Entities.Product product)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DataAccessLayer.Entities.Product, DTO.ProductDTO>());
            return Mapper.Map<DataAccessLayer.Entities.Product, DTO.ProductDTO>(product);
        }

        internal DTO.OperationDTO MappingToBL(DataAccessLayer.Entities.Operation operation)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DataAccessLayer.Entities.Operation, DTO.OperationDTO>());
            return Mapper.Map<DataAccessLayer.Entities.Operation, DTO.OperationDTO>(operation);
        }

        #endregion
    }
}
