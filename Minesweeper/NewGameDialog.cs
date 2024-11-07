namespace Minesweeper
{
    public partial class NewGameDialog : Form
    {
        public NewGameDialog()
        {
            InitializeComponent();
        }

        public int RowCount
        {
            get { return Convert.ToInt32(numericRows.Value); }
        }

        public int ColumnCount
        {
            get { return Convert.ToInt32(numericColumns.Value); }
        }

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
