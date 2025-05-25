using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Core
{
    [Serializable]
    public class GameState
    {
        public Cell[,] Cells { get; set; }
        public int GenerationCount { get; set; }
        public int TotalDeaths { get; set; }
    }
}
