namespace Minesweeper
{
    /// <summary>
    /// Represents minefield.
    /// </summary>
    public class Minefield
    {
        private static readonly Dictionary<Difficulty, double> difficultyLookup = new Dictionary<Difficulty, double>()
        {
            { Difficulty.Easy, 0.10 },
            { Difficulty.Medium, 0.25 },
            { Difficulty.Hard, 0.50 }
        };

        private Minefield(List<Row> rows, int mineCount)
        {
            Rows = rows;
            MineCount = mineCount;
        }

        /// <summary>
        /// Gets the rows in minefield.
        /// </summary>
        public IReadOnlyList<Row> Rows { get; }

        /// <summary>
        /// Gets the total count of mines in minefield.
        /// </summary>
        public int MineCount { get; private set; }

        /// <summary>
        /// Defines difficulty of the minefield.
        /// </summary>
        public enum Difficulty : int
        {
            /// <summary>
            /// Easy field.
            /// </summary>
            Easy = 0,

            /// <summary>
            /// Medium field.
            /// </summary>
            Medium = 1,

            /// <summary>
            /// Hard field.
            /// </summary>
            Hard = 2
        }

        /// <summary>
        /// Creates new <see cref="Minefield"/> for the game.
        /// </summary>
        /// <param name="rowCount">The count of rows.</param>
        /// <param name="columnsPerRow">The count of columns per row.</param>
        /// <param name="difficulty">The minefield difficulty.</param>
        /// <returns>A new <see cref="Minefield"/> instance.</returns>
        public static Minefield Create(int rowCount, int columnsPerRow, Difficulty difficulty)
        {
            // Calculate count of columns and total mines.
            int columnCount = rowCount * columnsPerRow;
            int totalMineCount = (int)Math.Round(columnCount * difficultyLookup[difficulty], MidpointRounding.AwayFromZero);

            // Initialize rows
            var rows = new List<Row>(rowCount);
            int rowIndex = 0;

            while (rows.Count < rowCount)
            {
                var columns = new List<Column>(columnsPerRow);

                int columnIndex = 0;

                while (columns.Count < columnsPerRow)
                {
                    columns.Add(new Column(columnIndex));
                    columnIndex++;
                }

                rows.Add(new Row(rowIndex, columns));
                rowIndex++;
            }

            // Initialize columns with mines.
            var diff = DateTime.Now.Subtract(DateTime.Today);

            var random = new Random((int)diff.Ticks);

            int mineCount = 0;

            while (mineCount < totalMineCount)
            {
                int r = random.Next(0, rowCount);
                int c = random.Next(0, columnsPerRow);
                var row = rows[r];
                var col = row.Columns[c];
                col.HasMine = true;
                mineCount++;
            }

            // Calculate mine counts.
            foreach (var row in rows)
            {
                foreach (var column in row.Columns)
                {
                    if (column.HasMine)
                        continue;

                    if (column.Index > 0)
                    {
                        Column prevColumn = row.Columns[column.Index - 1];
                        if (prevColumn.HasMine)
                            column.MineCount++;
                    }

                    if (column.Index < columnsPerRow - 1)
                    {
                        Column nextColumn = row.Columns[column.Index + 1];
                        if (nextColumn.HasMine)
                            column.MineCount++;
                    }

                    if (row.Index > 0)
                    {
                        Row prevRow = rows[row.Index - 1];
                        Column prevColumn = prevRow.Columns[column.Index];
                        if (prevColumn.HasMine)
                            column.MineCount++;
                        if (prevColumn.Index > 0)
                            if (prevRow.Columns[prevColumn.Index - 1].HasMine)
                                column.MineCount++;
                        if (prevColumn.Index < columnsPerRow - 1)
                            if (prevRow.Columns[prevColumn.Index + 1].HasMine)
                                column.MineCount++;
                    }

                    if (row.Index < rowCount - 1)
                    {
                        Row nextRow = rows[row.Index + 1];
                        Column nextColumn = nextRow.Columns[column.Index];
                        if (nextColumn.HasMine)
                            column.MineCount++;
                        if (nextColumn.Index > 0)
                            if (nextRow.Columns[nextColumn.Index - 1].HasMine)
                                column.MineCount++;
                        if (nextColumn.Index < columnsPerRow - 1)
                            if (nextRow.Columns[nextColumn.Index + 1].HasMine)
                                column.MineCount++;
                    }
                }
            }

            // Create minefield
            var minefield = new Minefield(rows, totalMineCount);

            return minefield;
        }

        /// <summary>
        /// Gets mine count of column specified by column index from row specified by row index.
        /// </summary>
        /// <param name="rowIndex">The row index.</param>
        /// <param name="columnIndex">The column index.</param>
        /// <param name="mine"><c>true</c> if specified column has mine; <c>false</c> otherwise.</param>
        /// <returns>A mine count of specified column.</returns>
        public int GetMineCount(int rowIndex, int columnIndex, out bool mine)
        {
            var column = GetColumn(rowIndex, columnIndex);
            mine = column.HasMine;
            return column.MineCount;
        }

        /// <summary>
        /// Gets column specified by column index from row specified by row index. 
        /// </summary>
        /// <param name="rowIndex">The row index.</param>
        /// <param name="columnIndex">The column index.</param>
        /// <returns>A <see cref="Column"/> at specified column index in specified row.</returns>
        public Column GetColumn(int rowIndex, int columnIndex)
        {
            var row = Rows[rowIndex];
            var column = row.Columns[columnIndex];
            return column;
        }

        /// <summary>
        /// Represents row in the minefield.
        /// </summary>
        public class Row
        {
            /// <summary>
            /// Initializes new <see cref="Row"/> instance.
            /// </summary>
            /// <param name="index">The row index.</param>
            /// <param name="columns">The columns in the row.</param>
            public Row(int index, List<Column> columns)
            {
                Index = index;
                Columns = columns.AsReadOnly();
            }

            /// <summary>
            /// Gets the row index.
            /// </summary>
            public int Index { get; }

            /// <summary>
            /// Gets the column in the row.
            /// </summary>
            public IReadOnlyList<Column> Columns { get; }
        }

        /// <summary>
        /// Represents column in the minefield.
        /// </summary>
        public class Column
        {
            /// <summary>
            /// Initializes new <see cref="Column"/>  instance.
            /// </summary>
            /// <param name="index">The column index.</param>
            public Column(int index)
            {
                Index = index;
            }

            /// <summary>
            /// Gets the column index.
            /// </summary>
            public int Index { get; }

            /// <summary>
            /// Gets whether or not column contains mine.
            /// </summary>
            public bool HasMine { get; internal set; } = false;

            /// <summary>
            /// Gets how many mines are around this column if itself has no mine.
            /// </summary>
            public int MineCount { get; internal set; } = 0;
        }
    }
}
