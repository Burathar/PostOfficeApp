using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace DAL.MySql
{
    internal class MySqlDatabase
    {
        private MySqlConnection connection;

        public MySqlDatabase()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            // MySqlCredentials.GetMySqlConnection() is not present in the git repository for it contains sensitive data.
            IMySqlCredentials credentials = new MySqlCredentials();
            connection = credentials.GetMySqlConnection();
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        throw new DalException("Cannot connect to server.  Contact administrator");

                    case 1045:
                        throw new DalException("Invalid username/password, please try again");
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                throw new DalException(ex.Message);
            }
        }

        public object Scalar(string query, List<MySqlParameter> parameters = null)
        {
            object result;
            OpenConnection();
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                AddParameters(parameters, cmd);
                try
                {
                    result = cmd.ExecuteScalar();
                }
                catch (MySqlException e)
                {
                    throw new DalException(e.Message, e);
                }
                catch (InvalidOperationException e)
                {
                    throw new DalException(e.Message, e);
                }
                CloseConnection();
            }

            if (result == null)
            {
                Console.WriteLine($"The query \"{query}\" didn't return a value");
            }
            return result;
        }

        //Update statement
        public void Update()
        {
            string query = "UPDATE tableinfo SET name='Joe', age='22' WHERE name='John Smith'";

            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Delete statement
        public void Delete()
        {
            string query = "DELETE FROM tableinfo WHERE name='John Smith'";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public bool NonQuery(string query, List<MySqlParameter> parameters = null)
        {
            OpenConnection();
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                AddParameters(parameters, cmd);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    throw new DalException(e.Message, e);
                }
                catch (InvalidOperationException e)
                {
                    throw new DalException(e.Message, e);
                }
                CloseConnection();
            }
            return true;
        }

        public bool NonQuery(string query, MySqlParameter parameter)
        {
            return NonQuery(query, new List<MySqlParameter> { parameter });
        }

        public DataTable Reader(string query, List<MySqlParameter> parameters = null)
        {
            DataTable table = new DataTable();

            OpenConnection();

            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                AddParameters(parameters, cmd);

                try
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        table.Load(reader);
                        reader.Close();
                    }
                }
                catch (MySqlException e)
                {
                    throw new DalException(e.Message, e);
                }
                catch (InvalidOperationException e)
                {
                    throw new DalException(e.Message, e);
                }
            }
            CloseConnection();

            return table;
        }

        public DataTable Reader(string query, MySqlParameter parameter)
        {
            return Reader(query, new List<MySqlParameter> { parameter });
        }

        private void AddParameters(IReadOnlyCollection<MySqlParameter> parameters, MySqlCommand cmd)
        {
            if (parameters == null) return;
            foreach (MySqlParameter parameter in parameters)
            {
                cmd.Parameters.Add(parameter);
            }
        }
    }
}