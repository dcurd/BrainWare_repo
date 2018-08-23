using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Infrastructure
{
    using System.Data.Common;
    using System.Data.SqlClient;

    public class Database
    {
        private SqlConnection _connection;
        private static Database instance = null;
        private static readonly object databaseObjectLock = new object();

        public static Database Instance{
            get{
                if(instance==null){
                    lock (databaseObjectLock){
                        if(instance==null){
                            instance = new Database();
                        }
                    }
                }
                return instance;
            }
        }

        private Database()
        {
            _connection = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=BrainWAre;Integrated Security=SSPI;AttachDBFilename=D:\\BrainWare\\Web\\App_Data\\BrainWare.mdf");

            _connection.Open();
        }


        public DbDataReader ExecuteReader(string query)
        {
           

            var sqlQuery = new SqlCommand(query, _connection);

            return sqlQuery.ExecuteReader();
        }

        public int ExecuteNonQuery(string query)
        {
            var sqlQuery = new SqlCommand(query, _connection);

            return sqlQuery.ExecuteNonQuery();
        }

        private void CheckConnection(){
            if(_connection.State==System.Data.ConnectionState.Closed || _connection == null){
                _connection = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=BrainWAre;Integrated Security=SSPI;AttachDBFilename=D:\\BrainWare\\Web\\App_Data\\BrainWare.mdf");
                _connection.Open();
            }
        }

    }
}