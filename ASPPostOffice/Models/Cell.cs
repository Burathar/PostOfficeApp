using System.Drawing;

namespace Models
{
    public class Cell
    {
        public Point Position;
        public bool Enabled;
        public int Number { get; private set; }

        public Cell(Point position, bool enabled, int number)
        {
            Position = position;
            Enabled = enabled;
            Number = number;
        }
    }
}