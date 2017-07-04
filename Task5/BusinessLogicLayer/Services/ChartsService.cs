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
        BLMapper MapperBL { get; set; }

        public ChartsService(IUnitOfWork uow)
        {
            Database = uow;
            MapperBL = new BLMapper();
        }

        public IEnumerable<ManagerDTOSumCost> GetSumCostManager()
        {
            try
            {
                IEnumerable<Operation> operations = Database.Operations.GetAll();
                IEnumerable<OperationDTO> operationDTOs = MapperBL.Mapping(operations);

                IEnumerable<Manager> managers = Database.Managers.GetAll();
                IEnumerable<ManagerDTOSumCost> managersSum = MapperBL.MappingGraph(managers);

                IEnumerable<IGrouping<int, string>> managersGr = managersSum.GroupBy(x => x.Id, y => y.Nickname);

                foreach (IGrouping<int, string> manager in managersGr)
                {
                    ManagerDTOSumCost man = managersSum.FirstOrDefault(item => item.Id == manager.Key);
                    man.SumCost = operationDTOs.Where(x => x.ManagerId == man.Id).Sum(y => y.Cost);
                }
                return managersSum;
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
