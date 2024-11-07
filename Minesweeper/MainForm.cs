namespace Minesweeper
{
    /// <summary>
    /// Represents main game form.
    /// </summary>
    public partial class MainForm : Form
    {
        private Minefield? minefield;
        private readonly TableLayoutPanel layoutPanel;
        private const int buttonWidth = 20;
        private const int buttonHeight = 20;
        private readonly ControlCache<Button> buttonCache;
        private readonly ControlCache<Label> labelCache;

        private static readonly Dictionary<int, Color> colors = new Dictionary<int, Color>()
        {
            { 1, Color.Green },
            { 2, Color.Green },
            { 3, Color.Blue },
            { 4, Color.Blue },
            { 5, Color.DeepPink },
            { 6, Color.DeepPink },
            { 7, Color.Red },
            { 8, Color.Red }
        };

        private int rowCount = 20;
        private int columnCount = 20;
        private Minefield.Difficulty difficulty = Minefield.Difficulty.Medium;

        /// <summary>
        /// Initializes new <see cref="MainForm"/> instance.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            buttonCache = new ControlCache<Button>();
            buttonCache.ControlCreated += OnButtonCreated;
            buttonCache.ControlDisposing += OnButtonDisposing;
            labelCache = new ControlCache<Label>();
            labelCache.ControlCreated += OnLabelCreated;
            layoutPanel = new TableLayoutPanel();
            layoutPanel.Dock = DockStyle.Fill;
            layoutPanel.Padding = new Padding(0);
            layoutPanel.Margin = new Padding(0);
            controlPanel.Controls.Add(layoutPanel);
        }

        private void OnLabelCreated(object? sender, ControlCreatedEventArgs<Label> e)
        {
            var label = e.Control;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Width = buttonWidth;
            label.Height = buttonHeight;
            label.Margin = new Padding(0);
            label.Padding = new Padding(0);
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new NewGameDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                rowCount = dialog.RowCount;
                columnCount = dialog.ColumnCount;
                difficulty = dialog.Difficulty;
                layoutPanel.Controls.Clear();
                layoutPanel.RowCount = rowCount;
                layoutPanel.ColumnCount = columnCount;
                CreateMinefield();
            }
        }

        private Minefield Minefield
        {
            get
            {
                if (minefield == null)
                    minefield = Minefield.Create(rowCount, columnCount, difficulty);

                return minefield;
            }
        }

        private void CreateMinefield()
        {
            controlPanel.Visible = false;
            if (minefield != null)
                minefield = null;
            labelCache.Reset();
            SuspendLayout();
            if (layoutPanel.Controls.Count > 0)
                layoutPanel.Controls.Clear();
            CreateButtons(rowCount, columnCount);
            controlPanel.Visible = true;
            ResumeLayout(true);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ResizeRedraw = true;
            CreateMinefield();
        }

        private void CreateButtons(int rowCount, int columnCount)
        {
            buttonCache.Reset();

            int rowIndex = 0;

            while (rowIndex < rowCount - 1)
            {
                int columnIndex = 0;

                while (columnIndex < columnCount - 1)
                {
                    var column = Minefield.GetColumn(rowIndex, columnIndex);
                    Button button = buttonCache.GetControl();
                    button.Tag = new ButtonTag(rowIndex, columnIndex, column);
                    layoutPanel.Controls.Add(button, columnIndex, rowIndex);
                    columnIndex++;
                }

                rowIndex++;
            }

            controlPanel.Controls.Add(layoutPanel);

            int height = (buttonHeight * rowCount) + menuStrip1.Height + buttonHeight + 2;
            int width = buttonWidth * columnCount;

            Size = new Size(width, height);
        }

        private void OnButtonClick(object? sender, EventArgs e)
        {
            if (sender is Button button && button.Tag is ButtonTag buttonTag)
            {
                Label label = labelCache.GetControl();

                if (buttonTag.Column.HasMine)
                {
                    GameOver(layoutPanel);
                    return;
                }
                else if (buttonTag.Column.MineCount > 0)
                {
                    var color = colors[buttonTag.Column.MineCount];
                    label.Text = buttonTag.Column.MineCount.ToString();
                    label.ForeColor = color;
                }

                layoutPanel.Controls.Remove(button);
                layoutPanel.Controls.Add(label, buttonTag.ColumnIndex, buttonTag.RowIndex);
            }
        }

        private void GameOver(TableLayoutPanel layoutPanel)
        {
            var buttons = new List<Button>(layoutPanel.Controls.Count);

            foreach (Control control in layoutPanel.Controls)
            {
                if (control is Button button)
                    buttons.Add(button);
            }

            foreach (var button in buttons)
            {
                layoutPanel.Controls.Remove(button);

                if (button.Tag is ButtonTag buttonTag)
                {
                    Label label = new Label();
                    label.TextAlign = ContentAlignment.MiddleCenter;

                    if (buttonTag.Column.HasMine)
                        label.Text = "X";
                    else if (buttonTag.Column.MineCount > 0)
                    {
                        var color = colors[buttonTag.Column.MineCount];
                        label.Text = buttonTag.Column.MineCount.ToString();
                        label.ForeColor = color;
                    }

                    layoutPanel.Controls.Remove(button);
                    label.Margin = button.Margin;
                    label.Padding = button.Padding;
                    label.Height = button.Height;
                    label.Width = button.Width;

                    layoutPanel.Controls.Add(label, buttonTag.ColumnIndex, buttonTag.RowIndex);
                }
            }

            var dialogResult = MessageBox.Show(this, "Game Over!", "Game Over", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation);

            if (dialogResult == DialogResult.Retry)
                CreateMinefield();
        }

        private void OnButtonDisposing(object? sender, ControlDisposingEventArgs<Button> e)
        {
            var button = e.Control;
            button.Click -= OnButtonClick;
        }

        private void OnButtonCreated(object? sender, ControlCreatedEventArgs<Button> e)
        {
            var button = e.Control;
            button.Width = buttonWidth;
            button.Height = buttonHeight;
            button.Margin = new Padding(0);
            button.Padding = new Padding(0);
            button.Click += OnButtonClick;
        }

        private class ButtonTag
        {
            public ButtonTag(int rowIndex, int columnIndex, Minefield.Column column)
            {
                RowIndex = rowIndex;
                ColumnIndex = columnIndex;
                Column = column;
            }

            public int RowIndex { get; }

            public int ColumnIndex { get; }

            public Minefield.Column Column { get; }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            layoutPanel.Controls.Clear();
            buttonCache.Clear();
            labelCache.Clear();
            layoutPanel.Dispose();
        }
    }
}