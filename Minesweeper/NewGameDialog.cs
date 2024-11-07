namespace Minesweeper
{
    /// <summary>
    /// Represents dialog to input settings of new game.
    /// </summary>
    public partial class NewGameDialog : Form
    {
        /// <summary>
        /// Initializes new <see cref="NewGameDialog"/> instance.
        /// </summary>
        public NewGameDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets the selected row count.
        /// </summary>
        public int RowCount
        {
            get { return Convert.ToInt32(numericRows.Value); }
        }

        /// <summary>
        /// Gets the selected column count.
        /// </summary>
        public int ColumnCount
        {
            get { return Convert.ToInt32(numericColumns.Value); }
        }

        /// <summary>
        /// Gets the selected minefield difficulty.
        /// </summary>
        public Minefield.Difficulty Difficulty
        {
            get
            {
                if (radioEasy.Checked)
                    return Minefield.Difficulty.Easy;
                else if (radioHard.Checked)
                    return Minefield.Difficulty.Hard;
                else
                    return Minefield.Difficulty.Medium;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
