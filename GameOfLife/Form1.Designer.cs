namespace GameOfLife
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this._startButton = new System.Windows.Forms.Button();
            this._stopButton = new System.Windows.Forms.Button();
            this._clearButton = new System.Windows.Forms.Button();
            this._randomizeButton = new System.Windows.Forms.Button();
            this._stepButton = new System.Windows.Forms.Button();
            this._saveButton = new System.Windows.Forms.Button();
            this._loadButton = new System.Windows.Forms.Button();
            this._speedTrackBar = new System.Windows.Forms.TrackBar();
            this._generationLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._aliveCellsLabel = new System.Windows.Forms.Label();
            this._deadCellsLabel = new System.Windows.Forms.Label();
            this._size25Button = new System.Windows.Forms.Button();
            this._size40Button = new System.Windows.Forms.Button();
            this._size50Button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._speedTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // _startButton
            // 
            this._startButton.BackColor = System.Drawing.Color.MistyRose;
            this._startButton.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._startButton.Location = new System.Drawing.Point(12, 12);
            this._startButton.Name = "_startButton";
            this._startButton.Size = new System.Drawing.Size(90, 40);
            this._startButton.TabIndex = 0;
            this._startButton.Text = "Start";
            this._startButton.UseVisualStyleBackColor = false;
            this._startButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // _stopButton
            // 
            this._stopButton.BackColor = System.Drawing.Color.MistyRose;
            this._stopButton.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._stopButton.Location = new System.Drawing.Point(108, 12);
            this._stopButton.Name = "_stopButton";
            this._stopButton.Size = new System.Drawing.Size(90, 40);
            this._stopButton.TabIndex = 1;
            this._stopButton.Text = "Stop";
            this._stopButton.UseVisualStyleBackColor = false;
            this._stopButton.Click += new System.EventHandler(this._stopButton_Click);
            // 
            // _clearButton
            // 
            this._clearButton.BackColor = System.Drawing.Color.MistyRose;
            this._clearButton.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._clearButton.Location = new System.Drawing.Point(300, 12);
            this._clearButton.Name = "_clearButton";
            this._clearButton.Size = new System.Drawing.Size(90, 40);
            this._clearButton.TabIndex = 2;
            this._clearButton.Text = "Clear";
            this._clearButton.UseVisualStyleBackColor = false;
            this._clearButton.Click += new System.EventHandler(this._clearButton_Click);
            // 
            // _randomizeButton
            // 
            this._randomizeButton.BackColor = System.Drawing.Color.MistyRose;
            this._randomizeButton.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._randomizeButton.Location = new System.Drawing.Point(588, 12);
            this._randomizeButton.Name = "_randomizeButton";
            this._randomizeButton.Size = new System.Drawing.Size(90, 40);
            this._randomizeButton.TabIndex = 3;
            this._randomizeButton.Text = "Random";
            this._randomizeButton.UseVisualStyleBackColor = false;
            this._randomizeButton.Click += new System.EventHandler(this._randomizeButton_Click);
            // 
            // _stepButton
            // 
            this._stepButton.BackColor = System.Drawing.Color.MistyRose;
            this._stepButton.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._stepButton.Location = new System.Drawing.Point(204, 12);
            this._stepButton.Name = "_stepButton";
            this._stepButton.Size = new System.Drawing.Size(90, 40);
            this._stepButton.TabIndex = 4;
            this._stepButton.Text = "Step";
            this._stepButton.UseVisualStyleBackColor = false;
            this._stepButton.Click += new System.EventHandler(this._stepButton_Click);
            // 
            // _saveButton
            // 
            this._saveButton.BackColor = System.Drawing.Color.MistyRose;
            this._saveButton.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._saveButton.Location = new System.Drawing.Point(396, 12);
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(90, 40);
            this._saveButton.TabIndex = 5;
            this._saveButton.Text = "Save";
            this._saveButton.UseVisualStyleBackColor = false;
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // _loadButton
            // 
            this._loadButton.BackColor = System.Drawing.Color.MistyRose;
            this._loadButton.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._loadButton.Location = new System.Drawing.Point(492, 12);
            this._loadButton.Name = "_loadButton";
            this._loadButton.Size = new System.Drawing.Size(90, 40);
            this._loadButton.TabIndex = 6;
            this._loadButton.Text = "Load";
            this._loadButton.UseVisualStyleBackColor = false;
            this._loadButton.Click += new System.EventHandler(this._loadButton_Click);
            // 
            // _speedTrackBar
            // 
            this._speedTrackBar.BackColor = System.Drawing.SystemColors.Control;
            this._speedTrackBar.Location = new System.Drawing.Point(840, 26);
            this._speedTrackBar.Name = "_speedTrackBar";
            this._speedTrackBar.Size = new System.Drawing.Size(148, 45);
            this._speedTrackBar.TabIndex = 7;
            this._speedTrackBar.Scroll += new System.EventHandler(this._speedTrackBar_Scroll);
            // 
            // _generationLabel
            // 
            this._generationLabel.AutoSize = true;
            this._generationLabel.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._generationLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this._generationLabel.Location = new System.Drawing.Point(776, 92);
            this._generationLabel.Name = "_generationLabel";
            this._generationLabel.Size = new System.Drawing.Size(107, 26);
            this._generationLabel.TabIndex = 8;
            this._generationLabel.Text = "Generation";
            this._generationLabel.Click += new System.EventHandler(this._generationLabel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(773, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 26);
            this.label1.TabIndex = 9;
            this.label1.Text = "Speed";
            // 
            // _aliveCellsLabel
            // 
            this._aliveCellsLabel.AutoSize = true;
            this._aliveCellsLabel.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._aliveCellsLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this._aliveCellsLabel.Location = new System.Drawing.Point(830, 137);
            this._aliveCellsLabel.Name = "_aliveCellsLabel";
            this._aliveCellsLabel.Size = new System.Drawing.Size(53, 26);
            this._aliveCellsLabel.TabIndex = 10;
            this._aliveCellsLabel.Text = "Alive";
            // 
            // _deadCellsLabel
            // 
            this._deadCellsLabel.AutoSize = true;
            this._deadCellsLabel.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._deadCellsLabel.ForeColor = System.Drawing.Color.Black;
            this._deadCellsLabel.Location = new System.Drawing.Point(827, 179);
            this._deadCellsLabel.Name = "_deadCellsLabel";
            this._deadCellsLabel.Size = new System.Drawing.Size(56, 26);
            this._deadCellsLabel.TabIndex = 11;
            this._deadCellsLabel.Text = "Dead";
            // 
            // _size25Button
            // 
            this._size25Button.BackColor = System.Drawing.Color.LightPink;
            this._size25Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this._size25Button.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._size25Button.Location = new System.Drawing.Point(26, 835);
            this._size25Button.Name = "_size25Button";
            this._size25Button.Size = new System.Drawing.Size(90, 40);
            this._size25Button.TabIndex = 12;
            this._size25Button.Text = "Size S";
            this._size25Button.UseVisualStyleBackColor = false;
            this._size25Button.Click += new System.EventHandler(this._size25Button_Click);
            // 
            // _size40Button
            // 
            this._size40Button.BackColor = System.Drawing.Color.LightPink;
            this._size40Button.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._size40Button.Location = new System.Drawing.Point(122, 835);
            this._size40Button.Name = "_size40Button";
            this._size40Button.Size = new System.Drawing.Size(90, 40);
            this._size40Button.TabIndex = 13;
            this._size40Button.Text = "Size M";
            this._size40Button.UseVisualStyleBackColor = false;
            this._size40Button.Click += new System.EventHandler(this._size40Button_Click);
            // 
            // _size50Button
            // 
            this._size50Button.BackColor = System.Drawing.Color.LightPink;
            this._size50Button.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._size50Button.Location = new System.Drawing.Point(218, 835);
            this._size50Button.Name = "_size50Button";
            this._size50Button.Size = new System.Drawing.Size(90, 40);
            this._size50Button.TabIndex = 14;
            this._size50Button.Text = "Size L";
            this._size50Button.UseVisualStyleBackColor = false;
            this._size50Button.Click += new System.EventHandler(this._size50Button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 900);
            this.Controls.Add(this._size50Button);
            this.Controls.Add(this._size40Button);
            this.Controls.Add(this._size25Button);
            this.Controls.Add(this._deadCellsLabel);
            this.Controls.Add(this._aliveCellsLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._generationLabel);
            this.Controls.Add(this._speedTrackBar);
            this.Controls.Add(this._loadButton);
            this.Controls.Add(this._saveButton);
            this.Controls.Add(this._stepButton);
            this.Controls.Add(this._randomizeButton);
            this.Controls.Add(this._clearButton);
            this.Controls.Add(this._stopButton);
            this.Controls.Add(this._startButton);
            this.Name = "Form1";
            this.Text = "GameOfLife";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this._speedTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _startButton;
        private System.Windows.Forms.Button _stopButton;
        private System.Windows.Forms.Button _clearButton;
        private System.Windows.Forms.Button _randomizeButton;
        private System.Windows.Forms.Button _stepButton;
        private System.Windows.Forms.Button _saveButton;
        private System.Windows.Forms.Button _loadButton;
        private System.Windows.Forms.TrackBar _speedTrackBar;
        private System.Windows.Forms.Label _generationLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label _aliveCellsLabel;
        private System.Windows.Forms.Label _deadCellsLabel;
        private System.Windows.Forms.Button _size40Button;
        private System.Windows.Forms.Button _size50Button;
        private System.Windows.Forms.Button _size25Button;
    }
}

