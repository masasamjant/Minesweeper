namespace Minesweeper
{
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

        public IReadOnlyList<Row> Rows { get; }

        public int MineCount { get; private set; }

        public enum Difficulty : int
        {
            Easy = 0,
            Medium = 1,
            Hard = 2
        }

        public static Minefield Create(int rowCount, int columnsPerRow, Difficulty difficulty)
        {
            int columnCount = rowCount * columnsPerRow;
            int totalMineCount = (int)Math.Round(columnCount * difficultyLookup[difficulty], MidpointRounding.AwayFromZero);

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

            var minefield = new Minefield(rows, totalMineCount);

            return minefield;
        }

        public int GetMineCount(int rowIndex, int columnIndex, out bool mine)
        {
            var column = GetColumn(rowIndex, columnIndex);
            mine = column.HasMine;
            return column.MineCount;
        }

        public Column GetColumn(int rowIndex, int columnIndex)
        {
            var row = Rows[rowIndex];
            var column = row.Columns[columnIndex];
            return column;
        }

        public class Row
        {
            public Row(int index, List<Column> columns)
            {
                Index = index;
                Columns = columns.AsReadOnly();
            }

            public int Index { get; }

            public IReadOnlyList<Column> Columns { get; }
        }

        public class Column
        {
            public Column(int index)
            {
                Index = index;
            }
            public int Index { get; }

            public bool HasMine { get; internal set; } = false;

            public int MineCount { get; internal set; } = 0;
        }
    }
}
