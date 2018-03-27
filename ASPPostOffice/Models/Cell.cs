using System.Drawing;

namespace Models
{
    public class Cell
    {
        public Point Position { get; set; }
        public bool Enabled { get; set; }
        public int Number { get; set; }

        public Cell(Point position, bool enabled, int number)
        {
            Position = position;
            Enabled = enabled;
            Number = number;
        }
    }
}