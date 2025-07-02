using ClientBtgApp.Base.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientBtgApp.Base.Repository.Interfaces
{
    public interface IClientRepository
    {
        void Update(Client obj);
        void Insert(Client obj);
        void Delete(Client obj);
        int DeleteAll();
        List<Client> GetAll();
    }
}
