using BusinessLogicLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IOperationService
    {
        ManagerDTO GetManager(int id);
        IEnumerable<ManagerDTO> GetManagers();

        ClientDTO GetClient(int id);
        IEnumerable<ClientDTO> GetClients();

        ProductDTO GetProduct(int id);
        IEnumerable<ProductDTO> GetProducts();

        OperationDTO GetOperation(int id);
        IEnumerable<OperationDTO> GetOperations();

        void Dispose();
    }
}
