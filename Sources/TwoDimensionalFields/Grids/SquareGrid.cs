using System;
using TwoDimensionalFields.Drawing;
using TwoDimensionalFields.Maps;

namespace TwoDimensionalFields.Grids
{
    public class SquareGrid : IGrid, IDrawable, ILayer
    {
        private readonly double edge;
        private readonly double[,] matrix;

        public SquareGrid(double[,] matrix, double edge)
        {
            this.matrix = matrix;
            this.edge = edge;
        }

        public Bounds Bounds
        {
            get
            {
                return new Bounds(
                    Position.X,
                    Position.Y,
                    Position.X + matrix.GetLength(0) * edge,
                    Position.Y - matrix.GetLength(1) * edge);
            }
        }

        public double[,] Grid
        {
            get { return matrix; }
        }

        public string Name { get; set; } = "Square grid";

        public (double X, double Y) Position { get; set; }
        public bool Selected { get; set; } = false;

        public bool Visible { get; set; } = true;

        public void Draw(IDrawer drawer)
        {
            drawer.Draw(this);
        }

        public double GetValue(double x, double y)
        {
            double dx = x - Position.X;
            double dy = Position.Y - y;

            return GetValueByIndex(dx / edge, dy / edge);
        }

        public double GetValueByIndex(double i, double j)
        {
            if (i < 0 || i >= matrix.GetLength(0) || j < 0 || j >= matrix.GetLength(1))
            {
                return double.NaN;
            }

            int iMin = Convert.ToInt32(Math.Floor(i));
            int iMai = iMin + 1;
            int jMin = Convert.ToInt32(Math.Floor(j));
            int jMai = jMin + 1;

            double z1 = matrix[iMin, jMai];
            double z2 = matrix[iMai, jMai];
            double z3 = matrix[iMai, jMin];
            double z4 = matrix[iMin, jMin];

            double z5 = (j - jMin) * (z2 - z1) + z1;
            double z6 = (j - jMin) * (z4 - z3) + z3;
            double value = (i - iMin) * (z6 - z5) + z5;

            return value;
        }
    }
}
