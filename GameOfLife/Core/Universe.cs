using System;
using System.Collections.Generic;

namespace GameOfLife.Core
{
    public class Universe
    {
        private Cell[,] _cells;
        private readonly List<bool[,]> _previousStates = new List<bool[,]>();
        private const int MaxStatesToTrack = 10;

        public int Width { get; private set; }
        public int Height { get; private set; }

        public Universe(int width, int height)
        {
            Width = width;
            Height = height;
            _cells = new Cell[width, height];
            InitializeCells();
        }

        private void InitializeCells()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    _cells[x, y] = new Cell(x, y, false);
                }
            }
        }

        public Cell[,] GetCells() => _cells;

        public int GetWidth()
        {
            return Width;
        }

        public int GetHeight()
        {
            return Height;
        }

        public void SetCells(Cell[,] newCells, int width, int height)
        {
            _cells = newCells;
            width = newCells.GetLength(0);
            height = newCells.GetLength(1);
        }

        public bool IsWithinBounds(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        public void ToggleCell(int x, int y)
        {
            if (IsWithinBounds(x, y))
            {
                _cells[x, y].IsAlive = !_cells[x, y].IsAlive;
            }
        }

        public void NextGeneration()
        {
            var newCells = new Cell[Width, Height];
            var currentState = GetCurrentState();

            if (_previousStates.Count >= MaxStatesToTrack)
            {
                _previousStates.RemoveAt(0);
            }
            _previousStates.Add(currentState);

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    int aliveNeighbors = CountAliveNeighbors(x, y);
                    newCells[x, y] = new Cell(x, y, _cells[x, y].NextState(aliveNeighbors));
                }
            }

            _cells = newCells;
        }

        public int CountAliveNeighbors(int x, int y)
        {
            int count = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;

                    int nx = x + i;
                    int ny = y + j;

                    if (IsWithinBounds(nx, ny) && _cells[nx, ny].IsAlive)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public int CountAliveCells()
        {
            int alive = 0;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (_cells[x, y].IsAlive) alive++;
                }
            }
            return alive;
        }

        public int CountDeaths(bool[,] previousState)
        {
            int deaths = 0;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (previousState[x, y] && !_cells[x, y].IsAlive)
                    {
                        deaths++;
                    }
                }
            }
            return deaths;
        }

        public bool[,] GetCurrentState()
        {
            var state = new bool[Width, Height];
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    state[x, y] = _cells[x, y].IsAlive;
                }
            }
            return state;
        }

        public bool IsExtinct()
        {
            return CountAliveCells() == 0;
        }

        public bool IsStable()
        {
            if (_previousStates.Count < 2) return false;

            var currentState = GetCurrentState();
            var previousState = _previousStates[_previousStates.Count - 1];

            return AreStatesEqual(currentState, previousState);
        }

        public bool IsPeriodic()
        {
            if (_previousStates.Count < 2) return false;

            var currentState = GetCurrentState();

            for (int i = 0; i < _previousStates.Count - 1; i++)
            {
                if (AreStatesEqual(currentState, _previousStates[i]))
                {
                    return true;
                }
            }

            return false;
        }

        public bool AreStatesEqual(bool[,] state1, bool[,] state2)
        {
            if (state1.GetLength(0) != state2.GetLength(0) ||
                state1.GetLength(1) != state2.GetLength(1))
            {
                return false;
            }

            for (int y = 0; y < state1.GetLength(1); y++)
            {
                for (int x = 0; x < state1.GetLength(0); x++)
                {
                    if (state1[x, y] != state2[x, y])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void Clear()
        {
            InitializeCells();
        }

        public void Randomize(Random random)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    _cells[x, y].IsAlive = random.Next(2) == 1;
                }
            }
        }

        public GameState SaveState(int generationCount, int totalDeaths)
        {
            var savedCells = new Cell[Width, Height];
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    savedCells[x, y] = new Cell(x, y, _cells[x, y].IsAlive);
                }
            }

            return new GameState
            {
                Cells = savedCells,
                GenerationCount = generationCount,
                TotalDeaths = totalDeaths
            };
        }

        public void LoadState(GameState state)
        {
            var savedCells = state.Cells;
            var newCells = new Cell[savedCells.GetLength(0), savedCells.GetLength(1)];

            for (int y = 0; y < savedCells.GetLength(1); y++)
            {
                for (int x = 0; x < savedCells.GetLength(0); x++)
                {
                    newCells[x, y] = new Cell(x, y, savedCells[x, y].IsAlive);
                }
            }

            SetCells(newCells, GetWidth(), GetHeight());
        }
    }
}