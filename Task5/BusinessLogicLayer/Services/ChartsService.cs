using AutoMapper;
using BusinessLogicLayer.DTO;
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
    public class ChartsService:IChartsService
    {
        IUnitOfWork Database { get; set; }

        public ChartsService(IUnitOfWork uow)
        {
            Database = uow;
        }

        private IEnumerable<OperationDTO> GetOperations()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Operation, OperationDTO>()
                .ForMember(x => x.ClientNickname, opt => opt.MapFrom(item => item.Client.Nickname))
                .ForMember(x => x.ManagerNickname, opt => opt.MapFrom(item => item.Manager.Nickname))
                .ForMember(x => x.ProductName, opt => opt.MapFrom(item => item.Product.Name))
                );
            return Mapper.Map<IEnumerable<Operation>, IEnumerable<OperationDTO>>(Database.Operations.GetAll());
        }

        private IEnumerable<ManagerDTO> GetManagers()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Manager, ManagerDTO>());
            return Mapper.Map<IEnumerable<Manager>, List<ManagerDTO>>(Database.Managers.GetAll());
        }
        public IEnumerable<ManagerDTOSumCost> GetSumCostManager()
        {
            IEnumerable<ManagerDTO> managerDTOs = GetManagers();
            IEnumerable<OperationDTO> operationDTOs=GetOperations();

            Mapper.Initialize(opt => opt.CreateMap<ManagerDTO, ManagerDTOSumCost>());
            IEnumerable<ManagerDTOSumCost> managers = Mapper.Map<IEnumerable<ManagerDTO>, IEnumerable<ManagerDTOSumCost>>(managerDTOs);

            IEnumerable<IGrouping<int,string>> managersGr= managers.GroupBy(x=>x.Id,y=>y.Nickname);

            foreach (IGrouping<int,string> manager in managersGr)
            {
                ManagerDTOSumCost man = managers.FirstOrDefault(item => item.Id == manager.Key);
                man.SumCost = operationDTOs.Where(x => x.ManagerId==man.Id).Sum(y => y.Cost);
            }
            return managers;
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
