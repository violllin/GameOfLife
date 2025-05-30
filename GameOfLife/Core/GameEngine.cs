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

        public void NextGeneration()
        {
            SaveCurrentState();
            _universe.NextGeneration();
            _generationCount++;

            var deaths = CalculateDeaths();
            _totalDeaths += deaths;

            GenerationCompleted?.Invoke(_generationCount);
            UpdateCellsCount();

            CheckGameEndConditions();
        }

        private void SaveCurrentState()
        {
            var cells = _universe.GetCells();
            for (int y = 0; y < _universe.Height; y++)
            {
                for (int x = 0; x < _universe.Width; x++)
                {
                    _previousState[x, y] = cells[x, y].IsAlive;
                }
            }
        }

        private int CalculateDeaths()
        {
            int deaths = 0;
            var cells = _universe.GetCells();

            for (int y = 0; y < _universe.Height; y++)
            {
                for (int x = 0; x < _universe.Width; x++)
                {
                    if (_previousState[x, y] && !cells[x, y].IsAlive)
                    {
                        deaths++;
                    }
                }
            }
            return deaths;
        }

        private void UpdateCellsCount()
        {
            int alive = 0;
            var cells = _universe.GetCells();

            for (int y = 0; y < _universe.Height; y++)
            {
                for (int x = 0; x < _universe.Width; x++)
                {
                    if (cells[x, y].IsAlive) alive++;
                }
            }

            CellsCountUpdated?.Invoke(alive, _totalDeaths);
        }

        private void CheckGameEndConditions()
        {
            if (_universe.IsExtinct())
            {
                throw new GameEndedException("All cells have died.", GameEndReason.Extinction);
            }
            else if (_universe.IsStable())
            {
                throw new GameEndedException("Stable configuration reached.", GameEndReason.Stability);
            }
            else if (_universe.IsPeriodic())
            {
                throw new GameEndedException("Periodic configuration detected.", GameEndReason.Periodicity);
            }
        }

        public void Randomize() => _universe.Randomize(_random);
        public void Clear() => _universe.Clear();
        public Cell[,] GetCells() => _universe.GetCells();
        public int Width => _universe.Width;
        public int Height => _universe.Height;
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
