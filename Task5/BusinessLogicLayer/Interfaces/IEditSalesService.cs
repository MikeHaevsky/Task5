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
        void EditManager(ManagerDTO managerDTO);
        void EditClient(ClientDTO clientDTO);
        void EditProduct(ProductDTO productDTO);
        void EditOperation(OperationDTO operationDTO);
        void Dispose();
    }
}
