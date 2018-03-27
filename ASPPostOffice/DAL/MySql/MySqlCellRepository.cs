using System.Collections.Generic;
using System.Data;
using System.Drawing;
using Models;

namespace DAL.MySql
{
    public class MySqlCellRepository : ICellRepository
    {
        private readonly MySqlCellContext _cellContext = new MySqlCellContext();

        public Grid GetGrid()
        {
            Grid grid = new Grid(new Size(0, 0));
            LoadGrid(grid);

            return grid;
        }

        private void LoadGrid(Grid grid)
        {
            DataTable table = _cellContext.GetCellsEnabled();
            List<Cell> cells = new List<Cell>();
            foreach (DataRow row in table.Rows)
            {
                cells.Add(new Cell(
                    new Point(
                        row["XPosition"].Int32(),
                        row["YPosition"].Int32()),
                    row["Enabled"].Bool(), -1)
                );
            }
            grid.SetCells(cells);
        }

        public void ToggleCell(int x, int y)
        {
            bool state = _cellContext.GetCellEnabled(x, y);
            _cellContext.SetCellEnabled(x, y, !state);
        }
    }
}