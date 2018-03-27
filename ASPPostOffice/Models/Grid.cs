using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Models
{
    public class Grid
    {
        private Cell[,] _cells;
        public Size Size { get; private set; }

        public Grid(Size size)
        {
            Size = size;
            _cells = new Cell[size.Width, size.Height];
        }

        public Cell GetCell(int x, int y)
        {
            Cell cell = _cells[x, y];
            if (cell != null) return cell;
            Cell newCell = new Cell(new Point(x, y), false, -1);
            SetCellNumber(newCell);
            return newCell;
        }

        public void SetCells(List<Cell> cells)
        {
            Size = new Size(
                cells.Max(c => c.Position.X),
                cells.Max(c => c.Position.Y));
            _cells = new Cell[Size.Width, Size.Height];
            foreach (Cell cell in cells)
            {
                SetCellNumber(cell);
                _cells[cell.Position.X, cell.Position.Y] = cell;
            }
        }

        public void AddCell(Cell cell)
        {
            SetCellNumber(cell);
            _cells[cell.Position.X, cell.Position.Y] = cell;
        }

        private void SetCellNumber(Cell cell)
        {
            cell.Number = (cell.Position.Y) * Size.Width + cell.Position.X + 1;
        }
    }
}