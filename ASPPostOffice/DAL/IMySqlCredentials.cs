using MySql.Data;
using MySql.Data.MySqlClient;

namespace DAL
{
    internal interface IMySqlCredentials
    {
        MySqlConnection GetMySqlConnection();
    }
}