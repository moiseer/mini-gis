using System;
using System.Drawing;
using TwoDimensionalFields.Drawing;
using TwoDimensionalFields.MapObjects;
using TwoDimensionalFields.Maps;

namespace TwoDimensionalFields.Grids
{
    public class SquareGrid : IGrid, IDrawable, ILayer
    {
        private readonly double?[,] grid;
        private Bounds bounds;
        private double? maxValue;
        private double? minValue;

        public SquareGrid(double?[,] matrix, Node<double> position, double edge) : this()
        {
            grid = matrix;
            Edge = edge;
            Position = position;
        }

        private SquareGrid()
        {
            Visible = true;
            Name = "Square grid";
            GridBitmap = new SquareGridBitmap(this)
            {
                MinColor = Color.Blue,
                MaxColor = Color.Red
            };
        }

        public Bounds Bounds => bounds ?? (bounds = GetBounds());
        public int ColumnCount => grid.GetLength(1);
        public double Edge { get; }
        public SquareGridBitmap GridBitmap { get; }
        public double Height => (RowCount - 1) * Edge;
        public double? MaxValue => maxValue ?? CalcAndSetValues().Max;
        public double? MinValue => minValue ?? CalcAndSetValues().Min;
        public string Name { get; set; }

        /// <summary>
        /// Xmin, Ymax
        /// </summary>
        public Node<double> Position { get; }

        public int RowCount => grid.GetLength(0);
        public bool Selected { get; set; }
        public bool Visible { get; set; }
        public double Width => (ColumnCount - 1) * Edge;

        public double? this[int i, int j] => grid[i, j];

        public (double i, double j) CoordinatesToIndexes(double x, double y)
        {
            double i = (Position.Y - y) / Edge;
            double j = (x - Position.X) / Edge;

            return (i, j);
        }

        public void Draw(IDrawer drawer) => drawer.Draw(this);

        public double? GetValue(double x, double y)
        {
            (double i, double j) = CoordinatesToIndexes(x, y);

            return GetValueByIndex(i, j);
        }

        public double? GetValueByIndex(double i, double j)
        {
            if (i < 0 || i >= RowCount || j < 0 || j >= ColumnCount)
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

        public (double x, double y) IndexesToCoordinates(double i, double j)
        {
            double x = j * Edge + Position.X;
            double y = Position.Y - i * Edge;

            return (x, y);
        }

        public void SetValue(int i, int j, double? value)
        {
            grid[i, j] = value;
            ValueChanged(value);
        }

        /*public MapObject Search(ISearcher<MapObject> searcher)
        {
            return searcher.Search(this);
        }*/

        private (double? Min, double? Max) CalcAndSetValues()
        {
            double? min = grid[0, 0];
            double? max = grid[0, 0];

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    if (grid[i, j] > max)
                    {
                        max = grid[i, j];
                    }
                    else if (grid[i, j] < min)
                    {
                        min = grid[i, j];
                    }
                }
            }

            minValue = min;
            maxValue = max;

            return (min, max);
        }

        private Bounds GetBounds()
        {
            return new Bounds(
                Position.X,
                Position.Y,
                Position.X + Width,
                Position.Y - Height
            );
        }

        private void ValueChanged(double? newValue)
        {
            minValue = minValue.HasValue ? newValue < minValue ? newValue : minValue : newValue;
            maxValue = maxValue.HasValue ? newValue > maxValue ? newValue : maxValue : newValue;
            GridBitmap.Clear();
        }
    }
}
