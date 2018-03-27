using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public interface ICellRepository
    {
        Grid GetGrid();

        void ToggleCell(int x, int y);
    }
}