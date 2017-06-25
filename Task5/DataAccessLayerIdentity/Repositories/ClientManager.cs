using DataAccessLayerIdentity.EF;
using DataAccessLayerIdentity.Entities;
using DataAccessLayerIdentity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerIdentity.Repositories
{
    public class ClientManager : IClientManager
    {
        public ApplicationContext Database { get; set; }
        public ClientManager(ApplicationContext db)
        {
            Database = db;
        }

        public void Create(ClientProfile item)
        {
            Database.ClientProfiles.Add(item);
            Database.SaveChanges();
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return Database.Users;
        }

        public ApplicationUser GetUser(string idUser)
        {
            return Database.Users.FirstOrDefault(item => item.Id == idUser);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
