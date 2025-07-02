using ClientBtgApp.Base.Models;
using ClientBtgApp.Base.Repository.Interfaces;
using SQLite;

namespace ClientBtgApp.Base.Repository
{
    class ClientRepository : IClientRepository
    {
        private SQLiteConnection _dbConection;

        public ClientRepository()
        {
            _dbConection = DbContext.DbContext.Current.Connection;
        }

        public List<Client> GetAll()
        {
            return _dbConection.Table<Client>().ToList();
        }

        public void Insert(Client obj)
        {
            try
            {
                if (obj == null)
                    throw new Exception("Dados de cliente nulos.");

                _dbConection.Insert(obj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Update(Client obj)
        {
            try
            {
                if (obj == null)
                    throw new Exception("Dados de cliente nulos.");

                _dbConection.Update(obj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void Delete(Client obj)
        {
            try
            {
                if (obj == null)
                    throw new Exception("Dados de cliente nulos.");

                _dbConection.Delete(obj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int  DeleteAll()
        {
            return _dbConection.DeleteAll<Client>();
        }
    }
}
