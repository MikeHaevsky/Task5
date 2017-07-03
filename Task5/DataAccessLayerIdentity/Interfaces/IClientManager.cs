using DataAccessLayerIdentity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerIdentity.Interfaces
{
    public interface IClientManager : IDisposable
    {
        void Create(ClientProfile clientProfile);
        void Update(ClientProfile clientProfile);
        void Delete(ClientProfile clientProfile);
        ApplicationUser GetUser(string idUser);
        IEnumerable<ApplicationUser> GetAllUsers();
    }
}
