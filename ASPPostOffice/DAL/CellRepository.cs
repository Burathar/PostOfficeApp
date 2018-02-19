using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public class CellRepository
    {
        private readonly CellContext _cellContext = new CellContext();

        public Grid GetGrid()
        {
            Grid grid = new Grid(new Size(10, 20));
            GetCells(grid);

            return grid;
        }

        private int GetNumber(int x, int y, int width)
        {
            return (y - 1) * width + x;
        }

        private void GetCells(Grid grid)
        {
            DataTable table = _cellContext.GetCellsEnabled();
            List<Cell> cells = new List<Cell>();
            foreach (DataRow row in table.Rows)
            {
                cells.Add(new Cell(
                    new Point(
                        row["XPosition"].Int32(),
                        row["YPosition"].Int32()),
                    row["Enabled"].Bool(),
                    GetNumber(row["XPosition"].Int32(), row["YPosition"].Int32(), grid.Size.Width)
                    )
                );
            }
            grid.Cells = cells;
        }

        public void ToggleCell(int x, int y)
        {
            bool state = _cellContext.GetCellEnabled(x, y);
            _cellContext.SetCellEnabled(x, y, !state);
        }
    }
}