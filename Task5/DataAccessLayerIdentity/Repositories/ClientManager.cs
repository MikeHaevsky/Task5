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

        public void Update(ClientProfile clientProfile)
        {
            ClientProfile client = Database.ClientProfiles.Find(clientProfile.Id);//FirstOrDefault(item => item.Id == clientProfile.Id);
            if (client != null)
            {
                client.Name = clientProfile.Name;
                client.Email = clientProfile.Email;
                client.Address = clientProfile.Address;

                Database.Entry<ClientProfile>(client).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                throw new ArgumentException("ClientProfile not found on dbo.ClienfProfiles");
            }
        }

        public void Create(ClientProfile clientProfile)
        {
            Database.ClientProfiles.Add(clientProfile);
        }

        public void Delete(ClientProfile clientProfile)
        {
            Database.ClientProfiles.Remove(clientProfile);
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return Database.Users;
        }

        public ApplicationUser GetUser(string idUser)
        {
            return Database.Users.Find(idUser);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
