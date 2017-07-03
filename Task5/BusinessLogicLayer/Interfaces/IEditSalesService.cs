using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IEditSalesService
    {
        OperationDetails EditManager(ManagerDTO managerDTO);
        OperationDetails EditClient(ClientDTO clientDTO);
        OperationDetails EditProduct(ProductDTO productDTO);
        OperationDetails EditOperation(OperationDTO operationDTO);
        void Dispose();
    }
}
