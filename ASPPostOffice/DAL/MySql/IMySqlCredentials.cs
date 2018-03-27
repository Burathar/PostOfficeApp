using MySql.Data;
using MySql.Data.MySqlClient;

namespace DAL.MySql
{
    internal interface IMySqlCredentials
    {
        MySqlConnection GetMySqlConnection();
    }
}