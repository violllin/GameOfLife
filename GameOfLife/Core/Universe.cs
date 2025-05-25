using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Core
{
    public class Universe
    {
        private int _width;
        private int _height;
        private Cell[,] _cells;
        private readonly List<Cell[,]> _previousGenerations = new List<Cell[,]>();

        public Universe(int width, int height)
        {
            _width = width;
            _height = height;
            _cells = new Cell[width, height];
            InitializeCells();
        }

        public int Width => _width;
        public int Height => _height;

        public void SetCells(Cell[,] cells)
        {
            _cells = new Cell[cells.GetLength(0), cells.GetLength(1)];
            for (int y = 0; y < cells.GetLength(1); y++)
            {
                for (int x = 0; x < cells.GetLength(0); x++)
                {
                    _cells[x, y] = new Cell(x, y, cells[x, y].IsAlive);
                }
            }
            _width = cells.GetLength(0);
            _height = cells.GetLength(1);
        }

        private void InitializeCells()
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    _cells[x, y] = new Cell(x, y);
                }
            }
        }

        public void Randomize(System.Random random)
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    _cells[x, y].IsAlive = random.Next(0, 2) == 1;
                }
            }
            _previousGenerations.Clear();
        }

        public void Clear()
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    _cells[x, y].IsAlive = false;
                }
            }
            _previousGenerations.Clear();
        }

        public void NextGeneration()
        {
            SaveCurrentGeneration();

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    int aliveNeighbors = CountAliveNeighbors(x, y);
                    _cells[x, y].NextState = _cells[x, y].IsAlive
                        ? aliveNeighbors == 2 || aliveNeighbors == 3
                        : aliveNeighbors == 3;
                }
            }

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    _cells[x, y].IsAlive = _cells[x, y].NextState;
                }
            }
        }

        private int CountAliveNeighbors(int x, int y)
        {
            int count = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;

                    int neighborX = x + i;
                    int neighborY = y + j;

                    if (neighborX >= 0 && neighborX < _width && neighborY >= 0 && neighborY < _height)
                    {
                        if (_cells[neighborX, neighborY].IsAlive)
                        {
                            count++;
                        }
                    }
                }
            }

            return count;
        }

        private void SaveCurrentGeneration()
        {
            var snapshot = new Cell[_width, _height];
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    snapshot[x, y] = new Cell(x, y, _cells[x, y].IsAlive);
                }
            }
            _previousGenerations.Add(snapshot);

            if (_previousGenerations.Count > 10)
            {
                _previousGenerations.RemoveAt(0);
            }
        }

        public bool IsStable()
        {
            if (_previousGenerations.Count < 2) return false;

            var current = _previousGenerations[_previousGenerations.Count - 1];
            var previous = _previousGenerations[_previousGenerations.Count - 2];

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    if (current[x, y].IsAlive != previous[x, y].IsAlive)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool IsExtinct()
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    if (_cells[x, y].IsAlive)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool IsPeriodic()
        {
            if (_previousGenerations.Count < 2) return false;

            var current = _previousGenerations[_previousGenerations.Count - 1];

            for (int i = 0; i < _previousGenerations.Count - 1; i++)
            {
                var previous = _previousGenerations[i];
                bool isSame = true;

                for (int y = 0; y < _height && isSame; y++)
                {
                    for (int x = 0; x < _width && isSame; x++)
                    {
                        if (current[x, y].IsAlive != previous[x, y].IsAlive)
                        {
                            isSame = false;
                        }
                    }
                }

                if (isSame)
                {
                    return true;
                }
            }

            return false;
        }

        public Cell[,] GetCells()
        {
            return _cells;
        }
    }
}
