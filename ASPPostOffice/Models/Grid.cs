using System.Collections.Generic;
using System.Drawing;

namespace Models
{
    public class Grid
    {
        public List<Cell> Cells;
        public Size Size { get; }

        public Grid(Size size)
        {
            Size = size;
            Cells = new List<Cell>();
        }
    }
}