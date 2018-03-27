using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Models;

namespace DAL.LocalFile
{
    public class LocalFileCellRepository : ICellRepository
    {
        private readonly string _filePath;

        public LocalFileCellRepository()
        {
            string initialPath = Path.GetTempPath();
            _filePath = Path.GetFullPath(Path.Combine(initialPath, @"PostOfficeGrid.txt"));
            if (!File.Exists(_filePath)) InitiateFile();
            if (File.ReadAllText(_filePath).Length < 10) InitiateFile();
        }

        private void InitiateFile()
        {
            SaveWholeGrid(new Grid(new Size(20, 10)));
        }

        public Grid GetGrid()
        {
            string fileText;
            try
            {
                fileText = File.ReadAllText(_filePath, Encoding.UTF8);
            }
            catch (Exception e)//TODO: Handle right exceptions
            {
                return null;
            }
            if (fileText.Length < 8) return null;
            string[] fileLines = Regex.Split(fileText, "\r\n|\r|\n");
            int width = 0, height = 0;
            Grid grid = null;
            int rowCounter = 0;
            foreach (string line in fileLines)
            {
                HandleLine(line, ref width, ref height, ref grid, ref rowCounter);
            }
            return grid;
        }

        private void HandleLine(string line, ref int width, ref int height, ref Grid grid, ref int rowCounter)
        {
            string tLine = line.Trim();
            if (tLine.Length < 1) return;
            if (grid != null)
            {
                GetGridRow(tLine, grid, ref rowCounter);
                return;
            }
            if (tLine.IndexOf("Width:", StringComparison.InvariantCultureIgnoreCase) == 0)
                width = Convert.ToInt32(tLine.Substring(tLine.IndexOf(':') + 1).Trim());
            else if (tLine.IndexOf("Height:", StringComparison.InvariantCultureIgnoreCase) == 0)
                height = Convert.ToInt32(tLine.Substring(tLine.IndexOf(':') + 1).Trim());
            if (width != 0 && height != 0 && grid == null) grid = new Grid(new Size(width, height));
        }

        private void GetGridRow(string tLine, Grid grid, ref int rowCounter)
        {
            for (int x = 0; x < tLine.Length; x++)
            {
                grid.AddCell(new Cell(new Point(x, rowCounter), tLine[x] == '1', -1));
            }
            rowCounter++;
        }

        public void ToggleCell(int x, int y)
        {
            Grid grid = GetGrid();
            grid.GetCell(x, y).Enabled = !grid.GetCell(x, y).Enabled;
            SaveWholeGrid(grid);
        }

        private void SaveWholeGrid(Grid grid)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Width: " + grid.Size.Width);
            sb.AppendLine("Height: " + grid.Size.Height);
            AppendGrid(grid, sb);
            using (StreamWriter sw = new StreamWriter(_filePath, false, Encoding.UTF8))
            {
                sw.Write(sb.ToString());
            }
        }

        private void AppendGrid(Grid grid, StringBuilder sb)
        {
            for (int y = 0; y < grid.Size.Height; y++)
            {
                for (int x = 0; x < grid.Size.Width; x++)
                {
                    Cell cell = grid.GetCell(x, y);
                    sb.Append(cell.Enabled ? '1' : '0');
                }
                sb.AppendLine();
            }
        }
    }
}