using System;
using TwoDimensionalFields.Drawing;
using TwoDimensionalFields.MapObjects;
using TwoDimensionalFields.Maps;

namespace TwoDimensionalFields.Grids
{
    public class SquareGrid : IGrid, IDrawable, ILayer
    {
        public double Edge { get; }

        public SquareGrid(double?[,] matrix, double edge)
        {
            Grid = matrix;
            Edge = edge;
        }

        public Bounds Bounds
        {
            get
            {
                return new Bounds(
                    Position.X,
                    Position.Y,
                    Position.X + Width,
                    Position.Y - Height);
            }
        }

        public double?[,] Grid { get; }

        public double Height
        {
            get { return (Grid.GetLength(1) - 1) * Edge; }
        }

        public string Name { get; set; } = "Square grid";

        public Node<double> Position { get; set; }

        public bool Selected { get; set; }

        public bool Visible { get; set; } = true;

        public double Width
        {
            get { return (Grid.GetLength(0) - 1) * Edge; }
        }

        public void Draw(IDrawer drawer)
        {
            drawer.Draw(this);
        }

        public double? GetValue(double x, double y)
        {
            double i = (x - Position.X) / Edge;
            double j = (Position.Y - y) / Edge;

            return GetValueByIndex(i, j);
        }

        public double? GetValueByIndex(double i, double j)
        {
            if (i < 0 || i >= Grid.GetLength(0) || j < 0 || j >= Grid.GetLength(1))
            {
                return null;
            }

            int iMin = Convert.ToInt32(Math.Floor(i));
            int iMax = iMin + 1;
            int jMin = Convert.ToInt32(Math.Floor(j));
            int jMax = jMin + 1;

            double? z1 = Grid[iMin, jMax];
            double? z2 = Grid[iMax, jMax];
            double? z3 = Grid[iMax, jMin];
            double? z4 = Grid[iMin, jMin];

            if (!z1.HasValue || !z2.HasValue || !z3.HasValue || !z4.HasValue)
            {
                return null;
            }

            double? z5 = (j - jMin) * (z2 - z1) + z1;
            double? z6 = (j - jMin) * (z4 - z3) + z3;
            double? value = (i - iMin) * (z6 - z5) + z5;

            return value;
        }

        /*public MapObject Search(ISearcher<MapObject> searcher)
        {
            return searcher.Search(this);
        }*/
    }
}
