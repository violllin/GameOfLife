using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Core
{
    public class GameEngine
    {
        private readonly Universe _universe;
        private readonly Random _random = new Random();
        private int _generationCount = 0;
        private int _totalDeaths = 0;
        private bool[,] _previousState;

        public event Action<int> GenerationCompleted;
        public event Action<int, int> CellsCountUpdated;

        public GameEngine(int width, int height)
        {
            _universe = new Universe(width, height);
            _previousState = new bool[width, height];
        }

        public void StartNewGame(int width, int height)
        {
            _universe.SetCells(new Cell[width, height], _universe.GetWidth(), _universe.GetHeight());
            _previousState = new bool[width, height];
            _generationCount = 0;
            _totalDeaths = 0;
        }
    }

    public class GameEndedException : Exception
    {
        public GameEndReason Reason { get; }

        public GameEndedException(string message, GameEndReason reason)
            : base(message)
        {
            Reason = reason;
        }
    }

    public enum GameEndReason
    {
        Extinction,
        Stability,
        Periodicity
    }
}
