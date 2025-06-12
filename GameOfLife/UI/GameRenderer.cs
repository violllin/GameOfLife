using GameOfLife.Core;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace GameOfLife.UI
{
    public enum ViewPerspective
    {
        TopLeft,     
        BottomLeft,  
        TopRight,   
        BottomRight  
    }

    public class GameRenderer
    {
        private readonly Universe _universe;
        private readonly int _cellSize;
        private readonly ViewPerspective _perspective;

        public GameRenderer(Universe universe, int cellSize, ViewPerspective perspective = ViewPerspective.TopLeft)
        {
            _universe = universe;
            _cellSize = cellSize;
            _perspective = perspective;
        }

        public void Render(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.None;
            g.Clear(Color.White);

            RenderGrid(g);
            RenderCells(g);
        }

        public void RenderGrid(Graphics g)
        {
            using (var gridPen = new Pen(Color.LightGray))
            {
                for (int y = 0; y <= _universe.Height; y++)
                {
                    float screenY = GetScreenY(y);
                    g.DrawLine(gridPen, 0, screenY, _universe.Width * _cellSize, screenY);
                }

                for (int x = 0; x <= _universe.Width; x++)
                {
                    float screenX = GetScreenX(x);
                    g.DrawLine(gridPen, screenX, 0, screenX, _universe.Height * _cellSize);
                }
            }
        }

        public void RenderCells(Graphics g)
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
                            RenderCell(g, cellBrush, x, y);
                        }
                    }
                }
            }
        }

        public void RenderCell(Graphics g, Brush brush, int x, int y)
        {
            float screenX = GetScreenX(x) + 1;
            float screenY = GetScreenY(y) + 1;
            float size = _cellSize - 1;

            g.FillRectangle(brush, screenX, screenY, size, size);
        }

        public float GetScreenX(int x)
        {
            switch (_perspective)
            {
                case ViewPerspective.TopRight:
                case ViewPerspective.BottomRight:
                    return (_universe.Width - x - 1) * _cellSize;
                default:
                    return x * _cellSize;
            }
        }

        public float GetScreenY(int y)
        {
            switch (_perspective)
            {
                case ViewPerspective.BottomLeft:
                case ViewPerspective.BottomRight:
                    return (_universe.Height - y - 1) * _cellSize;
                default:
                    return y * _cellSize;
            }
        }
    }
}