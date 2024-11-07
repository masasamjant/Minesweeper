namespace Minesweeper
{
    partial class NewGameDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            numericColumns = new NumericUpDown();
            numericRows = new NumericUpDown();
            label2 = new Label();
            label1 = new Label();
            groupBox2 = new GroupBox();
            radioHard = new RadioButton();
            radioMedium = new RadioButton();
            radioEasy = new RadioButton();
            buttonCancel = new Button();
            buttonOk = new Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericColumns).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericRows).BeginInit();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(numericColumns);
            groupBox1.Controls.Add(numericRows);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(261, 100);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Size";
            // 
            // numericColumns
            // 
            numericColumns.Location = new Point(110, 60);
            numericColumns.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            numericColumns.Name = "numericColumns";
            numericColumns.Size = new Size(120, 23);
            numericColumns.TabIndex = 4;
            numericColumns.Value = new decimal(new int[] { 20, 0, 0, 0 });
            // 
            // numericRows
            // 
            numericRows.Location = new Point(110, 30);
            numericRows.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            numericRows.Name = "numericRows";
            numericRows.Size = new Size(120, 23);
            numericRows.TabIndex = 3;
            numericRows.Value = new decimal(new int[] { 20, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(27, 62);
            label2.Name = "label2";
            label2.Size = new Size(58, 15);
            label2.TabIndex = 2;
            label2.Text = "Columns:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(27, 32);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 1;
            label1.Text = "Rows:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(radioHard);
            groupBox2.Controls.Add(radioMedium);
            groupBox2.Controls.Add(radioEasy);
            groupBox2.Location = new Point(12, 118);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(261, 114);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Difficulty";
            // 
            // radioHard
            // 
            radioHard.AutoSize = true;
            radioHard.Location = new Point(27, 76);
            radioHard.Name = "radioHard";
            radioHard.Size = new Size(51, 19);
            radioHard.TabIndex = 4;
            radioHard.Text = "Hard";
            radioHard.UseVisualStyleBackColor = true;
            // 
            // radioMedium
            // 
            radioMedium.AutoSize = true;
            radioMedium.Checked = true;
            radioMedium.Location = new Point(27, 51);
            radioMedium.Name = "radioMedium";
            radioMedium.Size = new Size(70, 19);
            radioMedium.TabIndex = 3;
            radioMedium.TabStop = true;
            radioMedium.Text = "Medium";
            radioMedium.UseVisualStyleBackColor = true;
            // 
            // radioEasy
            // 
            radioEasy.AutoSize = true;
            radioEasy.Location = new Point(27, 26);
            radioEasy.Name = "radioEasy";
            radioEasy.Size = new Size(48, 19);
            radioEasy.TabIndex = 2;
            radioEasy.Text = "Easy";
            radioEasy.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new Point(198, 238);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 2;
            buttonCancel.Text = "&Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // buttonOk
            // 
            buttonOk.Location = new Point(117, 238);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new Size(75, 23);
            buttonOk.TabIndex = 3;
            buttonOk.Text = "&Ok";
            buttonOk.UseVisualStyleBackColor = true;
            buttonOk.Click += buttonOk_Click;
            // 
            // NewGameDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(287, 271);
            Controls.Add(buttonOk);
            Controls.Add(buttonCancel);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Name = "NewGameDialog";
            Text = "New Game";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericColumns).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericRows).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private NumericUpDown numericRows;
        private Label label2;
        private Label label1;
        private NumericUpDown numericColumns;
        private GroupBox groupBox2;
        private RadioButton radioHard;
        private RadioButton radioMedium;
        private RadioButton radioEasy;
        private Button buttonCancel;
        private Button buttonOk;
    }
}