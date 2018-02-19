using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DAL
{
    internal class CellContext
    {
        private readonly MySqlDatabase _database = new MySqlDatabase();

        public DataTable GetCellsEnabled()
        {
            const string query = "SELECT Enabled, XPosition, YPosition FROM coc_dateing.GridCell;";
            return _database.Reader(query);
        }

        public bool GetCellEnabled(int x, int y)
        {
            const string query = "SELECT Enabled FROM coc_dateing.GridCell WHERE XPosition = @XPosition AND YPosition = @YPosition;";
            List<MySqlParameter> parameters = new List<MySqlParameter>
            {
                new MySqlParameter("@XPosition", x),
                new MySqlParameter("@YPosition", y)
            };
            return Convert.ToBoolean(_database.Scalar(query, parameters));
        }

        public bool SetCellEnabled(int x, int y, bool enabled)
        {
            const string query = "UPDATE GridCell SET Enabled = @Enabled WHERE XPosition = @XPosition AND YPosition = @YPosition;";
            List<MySqlParameter> parameters = new List<MySqlParameter>
            {
                new MySqlParameter("@XPosition", x),
                new MySqlParameter("@YPosition", y),
                new MySqlParameter("@Enabled", enabled)
            };
            return _database.NonQuery(query, parameters);
        }
    }
}