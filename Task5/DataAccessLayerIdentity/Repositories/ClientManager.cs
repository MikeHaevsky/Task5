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

        public async Task Update(ClientProfile clientProfile)
        {
            ClientProfile client = Database.ClientProfiles.FirstOrDefault(item => item.Id == clientProfile.Id);
            if (client != null)
            {
                client.Name = clientProfile.Name;
                client.Email = clientProfile.Email;
                client.Address = clientProfile.Address;
            }

            Database.Entry<ClientProfile>(client).State = System.Data.Entity.EntityState.Modified;
            await Database.SaveChangesAsync();
        }

        public async Task Create(ClientProfile clientProfile)
        {
            Database.ClientProfiles.Add(clientProfile);
            await Database.SaveChangesAsync();
        }

        public async Task Delete(ClientProfile clientProfile)
        {
            Database.ClientProfiles.Remove(clientProfile);
            await Database.SaveChangesAsync();
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
