using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IDBResult
    {
        ICollection<string> Errors { get; set; }
        bool Succeeded { get; set; }
    }
}
