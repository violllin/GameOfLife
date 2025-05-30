namespace GameOfLife.Core
{
    public class Cell
    {
        public int X { get; }
        public int Y { get; }
        public bool IsAlive { get; set; }

        public Cell(int x, int y, bool isAlive)
        {
            X = x;
            Y = y;
            IsAlive = isAlive;
        }

        public bool NextState(int aliveNeighbors)
        {
            if (IsAlive)
            {
                return aliveNeighbors == 2 || aliveNeighbors == 3;
            }
            else
            {
                return aliveNeighbors == 3;
            }
        }
    }
}