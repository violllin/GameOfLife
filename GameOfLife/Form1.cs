using GameOfLife.Core;
using GameOfLife.UI;
using System;
using System.Drawing;
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
            UpdateGenerationCount();
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

            _gamePanel.Paint += GamePanel_Paint;
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
            int alive = _universe.CountAliveCells();
            _aliveCellsLabel.Text = $"Живые клетки: {alive}";
            _deadCellsLabel.Text = $"Мертвые клетки: {_totalDeaths}";
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
            var renderer = new GameRenderer(_universe, CellSize);
            renderer.Render(e.Graphics);
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

            if (_universe.IsWithinBounds(cellX, cellY))
            {
                _universe.ToggleCell(cellX, cellY);
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
            _previousState = _universe.GetCurrentState();
            _universe.NextGeneration();
            _generationCount++;
            UpdateGenerationCount();

            _totalDeaths += _universe.CountDeaths(_previousState);
            _gamePanel.Invalidate();
            UpdateCellsCount();

            CheckGameEndConditions();
        }

        private void CheckGameEndConditions()
        {
            if (_universe.IsExtinct())
            {
                EndGame("All cells have died.");
            }
            else if (_universe.IsStable())
            {
                EndGame("Stable configuration reached.");
            }
            else if (_universe.IsPeriodic())
            {
                EndGame("Periodic configuration detected.");
            }
        }

        private void EndGame(string message)
        {
            _timer.Stop();
            RestoreUIAfterGameEnd();
            MessageBox.Show(message, "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            _timer.Interval = _speedTrackBar.Value;
            _timer.Start();
            _isRunning = true;
            UpdateButtonStates();
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
            UpdateGenerationCount();
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

            _savedState = _universe.SaveState(_generationCount, _totalDeaths);
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
                _universe.LoadState(_savedState);
                _generationCount = _savedState.GenerationCount;
                _totalDeaths = _savedState.TotalDeaths;
                UpdateGenerationCount();
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
            UpdateGenerationCount();
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

        private void RestoreUIAfterGameEnd()
        {
            _isRunning = false;
            UpdateButtonStates();
        }

        private void UpdateButtonStates()
        {
            _startButton.Enabled = !_isRunning;
            _stopButton.Enabled = _isRunning;
            _randomizeButton.Enabled = !_isRunning;
            _clearButton.Enabled = !_isRunning;
            _stepButton.Enabled = !_isRunning;
            _saveButton.Enabled = !_isRunning;
            _loadButton.Enabled = !_isRunning;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void _helpButton_Click(object sender, EventArgs e)
        {
            var helpForm = new HelpForm();
            helpForm.ShowDialog();
        }
        private void UpdateGenerationCount()
        {
            _generationLabel.Text = $"Поколение: {_generationCount}";
        }
    }
}