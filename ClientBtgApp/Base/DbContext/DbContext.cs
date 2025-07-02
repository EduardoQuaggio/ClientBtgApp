using ClientBtgApp.Base.Models;
using SQLite;

namespace ClientBtgApp.Base.DbContext
{
    public class DbContext
    {

        private static DbContext _lazy;
        private static SQLiteConnection _sqlConnection;
        private const string _dbName = "list_client_db.db3";

        public static DbContext Current
        {
            get
            {
                if (_lazy == null)
                    _lazy = new DbContext();

                return _lazy;
            }
        }

        private DbContext()
        {
            _sqlConnection = new SQLiteConnection(Path.Combine(FileSystem.AppDataDirectory, _dbName));
            _sqlConnection.CreateTable<Client>();
        }

        public SQLiteConnection Connection
        {
            get { return _sqlConnection; }
            set { _sqlConnection = value; }
        }
    }
}

