﻿using BusinessLogicLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IChartsService
    {
        IEnumerable<ManagerDTOSumCost> GetSumCostManager();
        void Dispose();
    }
}
