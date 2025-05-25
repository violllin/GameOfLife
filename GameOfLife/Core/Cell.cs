using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Core
{
    public class Cell
    {
        public bool IsAlive { get; set; }
        public bool NextState { get; set; }
        public int X { get; }
        public int Y { get; }

        public Cell(int x, int y, bool isAlive = false)
        {
            X = x;
            Y = y;
            IsAlive = isAlive;
            NextState = false;
        }
    }
}
