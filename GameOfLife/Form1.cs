using GameOfLife.Core;
using GameOfLife.UI;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GameOfLife
{
    partial class Form1 : Form
    {
        private const int CellSize = 15;
        private const int TimerInterval = 200;
        private Universe _universe;
        private readonly Random _random = new Random();
        private readonly Timer _timer = new Timer();
        private int _generationCount = 0;
        private bool _isRunning = false;
        private bool _isDrawing = false;
        private int _totalDeaths = 0;
        private bool[,] _previousState;
        private GameState _savedState;
        private BufferedPanel _gamePanel;

        public Form1()
        {
            InitializeComponent();
            InitializeGamePanel(40, 40);
            InitializeControls();
            UpdateCellsCount();
        }

        private void InitializeGamePanel(int width, int height)
        {
            if (_gamePanel != null)
            {
                Controls.Remove(_gamePanel);
                _gamePanel.Dispose();
            }

            _universe = new Universe(width, height);
            _totalDeaths = 0;
            _previousState = new bool[width, height];

            _gamePanel = new BufferedPanel
            {
                Location = new Point(10, 100),
                Size = new Size(width * CellSize + 1, height * CellSize + 1),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White 
            };

            _gamePanel.Paint += (sender, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.None;
                g.Clear(Color.White); 

                using (var gridPen = new Pen(Color.LightGray))
                {
                    for (int y = 0; y <= height; y++)
                        g.DrawLine(gridPen, 0, y * CellSize, width * CellSize, y * CellSize);

                    for (int x = 0; x <= width; x++)
                        g.DrawLine(gridPen, x * CellSize, 0, x * CellSize, height * CellSize);
                }

                var cells = _universe.GetCells();
                using (var cellBrush = new SolidBrush(Color.HotPink))
                {
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            if (cells[x, y].IsAlive)
                            {
                                g.FillRectangle(cellBrush,
                                              x * CellSize + 1,
                                              y * CellSize + 1,
                                              CellSize - 1,
                                              CellSize - 1);
                            }
                        }
                    }
                }
            };

            _gamePanel.MouseDown += GamePanel_MouseDown;
            _gamePanel.MouseMove += GamePanel_MouseMove;
            _gamePanel.MouseUp += GamePanel_MouseUp;

            Controls.Add(_gamePanel);
        }

        private void InitializeControls()
        {
            _speedTrackBar.ValueChanged += _speedTrackBar_Scroll;

            _timer.Interval = TimerInterval;
            _timer.Tick += Timer_Tick;
        }

        private void UpdateCellsCount()
        {
            int alive = 0;
            var cells = _universe.GetCells();

            for (int y = 0; y < _universe.Height; y++)
            {
                for (int x = 0; x < _universe.Width; x++)
                {
                    if (cells[x, y].IsAlive)
                        alive++;
                }
            }

            _aliveCellsLabel.Text = $"Alive: {alive}";
            _deadCellsLabel.Text = $"Deads: {_totalDeaths}";
        }

        private void ChangeGridSize(int width, int height)
        {
            if (_isRunning)
            {
                MessageBox.Show("Please stop the simulation before changing grid size.", "Warning");
                return;
            }

            InitializeGamePanel(width, height);
            _generationCount = 0;
            
            UpdateCellsCount();

            _size25Button.Top = _gamePanel.Bottom + 10;
            _size40Button.Top = _gamePanel.Bottom + 10;
            _size50Button.Top = _gamePanel.Bottom + 10;
        }

        private void GamePanel_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.None;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = PixelOffsetMode.Half;

            g.Clear(Color.White);

            using (var gridPen = new Pen(Color.LightGray))
            {
                for (int y = 0; y <= _universe.Height; y++)
                {
                    g.DrawLine(gridPen, 0, y * CellSize, _universe.Width * CellSize, y * CellSize);
                }

                for (int x = 0; x <= _universe.Width; x++)
                {
                    g.DrawLine(gridPen, x * CellSize, 0, x * CellSize, _universe.Height * CellSize);
                }
            }

            var cells = _universe.GetCells();
            using (var cellBrush = new SolidBrush(Color.Black))
            {
                for (int y = 0; y < _universe.Height; y++)
                {
                    for (int x = 0; x < _universe.Width; x++)
                    {
                        if (cells[x, y].IsAlive)
                        {
                            g.FillRectangle(cellBrush, x * CellSize + 1, y * CellSize + 1, CellSize - 1, CellSize - 1);
                        }
                    }
                }
            }
        }

        private void GamePanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !_isRunning)
            {
                _isDrawing = true;
                ToggleCell(e.X, e.Y);
            }
        }

        private void GamePanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDrawing && !_isRunning)
            {
                ToggleCell(e.X, e.Y);
            }
        }

        private void GamePanel_MouseUp(object sender, MouseEventArgs e)
        {
            _isDrawing = false;
        }

        private void ToggleCell(int x, int y)
        {
            int cellX = x / CellSize;
            int cellY = y / CellSize;

            if (cellX >= 0 && cellX < _universe.Width && cellY >= 0 && cellY < _universe.Height)
            {
                var cells = _universe.GetCells();
                cells[cellX, cellY].IsAlive = !cells[cellX, cellY].IsAlive;

                var rect = new Rectangle(cellX * CellSize + 1, cellY * CellSize + 1, CellSize - 1, CellSize - 1);
                _gamePanel.Invalidate(rect);
                _gamePanel.Update();

                UpdateCellsCount();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            NextGeneration();
        }

        private void NextGeneration()
        {
            var cells = _universe.GetCells();
            for (int y = 0; y < _universe.Height; y++)
            {
                for (int x = 0; x < _universe.Width; x++)
                {
                    _previousState[x, y] = cells[x, y].IsAlive;
                }
            }

            _universe.NextGeneration();
            _generationCount++;
            _generationLabel.Text = $"Generation: {_generationCount}";

            int deathsInThisGeneration = 0;
            for (int y = 0; y < _universe.Height; y++)
            {
                for (int x = 0; x < _universe.Width; x++)
                {
                    if (_previousState[x, y] && !cells[x, y].IsAlive)
                    {
                        deathsInThisGeneration++;
                    }
                }
            }
            _totalDeaths += deathsInThisGeneration;

            _gamePanel.Invalidate();
            UpdateCellsCount();

            if (_universe.IsExtinct())
            {
                _timer.Stop();
                RestoreUIAfterGameEnd();
                MessageBox.Show("All cells have died.", "Game Over",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (_universe.IsStable())
            {
                _timer.Stop();
                RestoreUIAfterGameEnd();
                MessageBox.Show("Stable configuration reached.", "Game Over",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (_universe.IsPeriodic())
            {
                _timer.Stop();
                RestoreUIAfterGameEnd();
                MessageBox.Show("Periodic configuration detected.", "Game Over",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            _timer.Interval = _speedTrackBar.Value;
            _timer.Start();

            _isRunning = true;
            _startButton.Enabled = false;
            _stopButton.Enabled = true;
            _randomizeButton.Enabled = false;
            _clearButton.Enabled = false;
            _stepButton.Enabled = false;
            _saveButton.Enabled = false;
            _loadButton.Enabled = false;
        }

        private void _stopButton_Click(object sender, EventArgs e)
        {
            _timer.Stop();
            RestoreUIAfterGameEnd();
        }

        private void _stepButton_Click(object sender, EventArgs e)
        {
            if (_isRunning)
            {
                MessageBox.Show("Please stop the simulation before using step mode.", "Warning");
                return;
            }

            NextGeneration();
        }

        private void _clearButton_Click(object sender, EventArgs e)
        {
            _universe.Clear();
            _generationCount = 0;
            _totalDeaths = 0;
            _generationLabel.Text = $"Generation: {_generationCount}";
            _gamePanel.Invalidate();
            UpdateCellsCount();
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            if (_isRunning)
            {
                MessageBox.Show("Please stop the simulation before saving.", "Warning");
                return;
            }

            var cells = _universe.GetCells();
            var savedCells = new Cell[cells.GetLength(0), cells.GetLength(1)];

            for (int y = 0; y < _universe.Height; y++)
            {
                for (int x = 0; x < _universe.Width; x++)
                {
                    savedCells[x, y] = new Cell(x, y, cells[x, y].IsAlive);
                }
            }

            _savedState = new GameState
            {
                Cells = savedCells,
                GenerationCount = _generationCount,
                TotalDeaths = _totalDeaths
            };

            MessageBox.Show("Game state saved in memory!", "Success");
        }

        private void _loadButton_Click(object sender, EventArgs e)
        {
            if (_isRunning)
            {
                MessageBox.Show("Please stop the simulation before loading.", "Warning");
                return;
            }

            if (_savedState == null)
            {
                MessageBox.Show("No saved state found in memory.", "Error");
                return;
            }

            try
            {
                int width = _savedState.Cells.GetLength(0);
                int height = _savedState.Cells.GetLength(1);
                InitializeGamePanel(width, height);

                var savedCells = _savedState.Cells;
                var newCells = new Cell[width, height];

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        newCells[x, y] = new Cell(x, y, savedCells[x, y].IsAlive);
                    }
                }

                _universe.SetCells(newCells);
                _generationCount = _savedState.GenerationCount;
                _totalDeaths = _savedState.TotalDeaths;

                _generationLabel.Text = $"Generation: {_generationCount}";
                _gamePanel.Invalidate();
                UpdateCellsCount();

                MessageBox.Show("Game state loaded from memory!", "Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading state: {ex.Message}", "Error");
            }
        }

        private void _randomizeButton_Click(object sender, EventArgs e)
        {
            _universe.Randomize(_random);
            _generationCount = 0;
            _totalDeaths = 0;
            _generationLabel.Text = $"Generation: {_generationCount}";
            _gamePanel.Invalidate();
            UpdateCellsCount();
        }

        private void _speedTrackBar_Scroll(object sender, EventArgs e)
        {
            if (_isRunning)
            {
                _timer.Interval = _speedTrackBar.Value;
            }

        }

        private void _size25Button_Click(object sender, EventArgs e)
        {
            ChangeGridSize(25, 25);
        }

        private void _size40Button_Click(object sender, EventArgs e)
        {
            ChangeGridSize(40, 40);
        }

        private void _size50Button_Click(object sender, EventArgs e)
        {
            ChangeGridSize(50, 50);
        }

        private void _generationLabel_Click(object sender, EventArgs e)
        {
            _generationLabel.Text = $"Generation: {_generationCount}";
        }

        private void RestoreUIAfterGameEnd()
        {
            _isRunning = false;
            _startButton.Enabled = true;
            _stopButton.Enabled = false;
            _randomizeButton.Enabled = true;
            _clearButton.Enabled = true;
            _stepButton.Enabled = true;
            _saveButton.Enabled = true;
            _loadButton.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
