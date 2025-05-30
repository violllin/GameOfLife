namespace GameOfLife.Core
{
    public class GameState
    {
        public Cell[,] Cells { get; set; }
        public int GenerationCount { get; set; }
        public int TotalDeaths { get; set; }
    }
}