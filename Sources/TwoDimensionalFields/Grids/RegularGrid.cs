using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TwoDimensionalFields.Drawing.GridGraphics;
using TwoDimensionalFields.MapObjects;
using TwoDimensionalFields.Maps;

namespace TwoDimensionalFields.Grids
{
    public class RegularGrid : Grid
    {
        private readonly double?[,] grid;

        public RegularGrid(double?[,] matrix, Node<double> position, double step) : this()
        {
            grid = matrix;
            Step = step;
            Position = position;

            CalcAndSetParams();
        }

        private RegularGrid()
        {
            Visible = true;
            Name = "Regular grid";
            GridGraphics = new RegularGridGraphics(this);
        }

        public int ColumnCount => grid.GetLength(1);
        public RegularGridGraphics GridGraphics { get; }
        public double Height => (RowCount - 1) * Step;

        /// <summary>
        /// Xmin, Ymax
        /// </summary>
        public Node<double> Position { get; }

        public int RowCount => grid.GetLength(0);
        public double Step { get; }
        public double Width => (ColumnCount - 1) * Step;

        public double? this[int i, int j] => grid[i, j];

        public (double i, double j) CoordinatesToIndexes(double x, double y)
        {
            double i = (Position.Y - y) / Step;
            double j = (x - Position.X) / Step;

            return (i, j);
        }

        public override double? GetValue(double x, double y)
        {
            (double i, double j) = CoordinatesToIndexes(x, y);

            return GetValueByIndex(i, j);
        }

        public double? GetValueByIndex(double i, double j)
        {
            if (i < 0 || i >= RowCount - 1 || j < 0 || j >= ColumnCount - 1)
            {
                return null;
            }

            int iMin = Convert.ToInt32(Math.Floor(i));
            int iMax = iMin + 1;
            int jMin = Convert.ToInt32(Math.Floor(j));
            int jMax = jMin + 1;

            double? z1 = grid[iMin, jMax];
            double? z2 = grid[iMax, jMax];
            double? z3 = grid[iMax, jMin];
            double? z4 = grid[iMin, jMin];

            if (!z1.HasValue || !z2.HasValue || !z3.HasValue || !z4.HasValue)
            {
                return null;
            }

            double? z5 = (j - jMin) * (z2 - z1) + z1;
            double? z6 = (j - jMin) * (z4 - z3) + z3;
            double? value = (i - iMin) * (z6 - z5) + z5;

            return value;
        }

        public Node<double> IndexesToCoordinates(double i, double j)
        {
            double x = j * Step + Position.X;
            double y = Position.Y - i * Step;

            return new Node<double>(x, y);
        }

        public override void SetColors(Dictionary<double, Color> colors) => GridGraphics.Colors = colors;

        public void SetValue(int i, int j, double? value)
        {
            grid[i, j] = value;
            ValueChanged();
        }

        protected override Bounds GetBounds()
        {
            return new Bounds(
                Position.X,
                Position.Y,
                Position.X + Width,
                Position.Y - Height
            );
        }

        protected override double? GetMaxValue() => grid.Cast<double?>().Where(value => value.HasValue).Max();
        protected override double? GetMinValue() => grid.Cast<double?>().Where(value => value.HasValue).Min();

        private void CalcAndSetParams()
        {
            if (grid.Length == 0)
            {
                return;
            }

            var min = grid[0, 0];
            var max = grid[0, 0];

            foreach (var value in grid)
            {
                min = min.HasValue && min < value ? min : value;
                max = max.HasValue && max > value ? max : value;
            }

            MinValue = min;
            MaxValue = max;
        }

        private void ValueChanged()
        {
            MinValue = null;
            MaxValue = null;
            GridGraphics.Clear();
        }
    }
}
