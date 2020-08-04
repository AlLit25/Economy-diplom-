using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mainAppDiplom
{
    class DB
    {
        
        SQLiteConnection connection = new SQLiteConnection("DataSource = E:/diplom(order)/economy-diplom-/mainAppDiplom/mainAppDiplom/dataDB.db");

        public void openConn()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }
        public void closeConn()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }
        public SQLiteConnection getConn()
        {
            return connection;
        }
    }
}
