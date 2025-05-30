using GameOfLife.Core;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace GameOfLife.UI
{
    public class GameRenderer
    {
        private readonly Universe _universe;
        private readonly int _cellSize;

        public GameRenderer(Universe universe, int cellSize)
        {
            _universe = universe;
            _cellSize = cellSize;
        }

        public void Render(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.None;
            g.Clear(Color.White);

            RenderGrid(g);
            RenderCells(g);
        }

        private void RenderGrid(Graphics g)
        {
            using (var gridPen = new Pen(Color.LightGray))
            {
                for (int y = 0; y <= _universe.Height; y++)
                    g.DrawLine(gridPen, 0, y * _cellSize, _universe.Width * _cellSize, y * _cellSize);

                for (int x = 0; x <= _universe.Width; x++)
                    g.DrawLine(gridPen, x * _cellSize, 0, x * _cellSize, _universe.Height * _cellSize);
            }
        }

        private void RenderCells(Graphics g)
        {
            var cells = _universe.GetCells();
            using (var cellBrush = new SolidBrush(Color.HotPink))
            {
                for (int y = 0; y < _universe.Height; y++)
                {
                    for (int x = 0; x < _universe.Width; x++)
                    {
                        if (cells[x, y].IsAlive)
                        {
                            g.FillRectangle(cellBrush,
                                          x * _cellSize + 1,
                                          y * _cellSize + 1,
                                          _cellSize - 1,
                                          _cellSize - 1);
                        }
                    }
                }
            }
        }
    }
}