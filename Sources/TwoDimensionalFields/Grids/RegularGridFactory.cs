using System;
using System.Linq;
using TwoDimensionalFields.MapObjects;
using TwoDimensionalFields.Searching;

namespace TwoDimensionalFields.Grids
{
    public class RegularGridFactory
    {
        public static RegularGrid Create(IrregularGrid irregularGrid, int edge, ValueCalculating calculatingType)
        {
            double delta = GetDelta(irregularGrid, calculatingType);
            return Create(irregularGrid, edge, delta, 2, calculatingType);
        }

        public static RegularGrid Create(IrregularGrid irregularGrid, int edge, double delta, int pow, ValueCalculating calculatingType)
        {
            double xMin = irregularGrid.Bounds.XMin;
            double xMax = irregularGrid.Bounds.XMax;
            double yMin = irregularGrid.Bounds.YMin;
            double yMax = irregularGrid.Bounds.YMax;

            var position = new Node<double>(xMin, yMax);
            var rowCount = Convert.ToInt32(Math.Floor(yMax - yMin) / edge) + 1;
            var columnCount = Convert.ToInt32(Math.Floor(xMax - xMin) / edge) + 1;

            return Create(irregularGrid, edge, position, rowCount, columnCount, delta, pow, calculatingType);
        }

        public static RegularGrid Create(IrregularGrid irregularGrid, int edge, Node<double> position, int rowCount, int columnCount, double delta, int pow, ValueCalculating calculatingType)
        {
            var grid = new double?[rowCount, columnCount];
            var squareGrid = new RegularGrid(grid, position, edge);

            var getValueFunc = GridValueSearcher.GetSearchingFunc(irregularGrid, calculatingType);

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    var searchPoint = squareGrid.IndexesToCoordinates(i, j);
                    double? value = getValueFunc(searchPoint, delta, pow);
                    squareGrid.SetValue(i, j, value);
                }
            }

            return squareGrid;
        }

        public static RegularGrid CreateTestGrid()
        {
            double?[,] testSurface = GenerateTestSurface();
            var edge = 2;
            var position = new Node<double>(
                -edge * testSurface.GetLength(0) / 2.0,
                edge * testSurface.GetLength(1) / 2.0
            );

            return new RegularGrid(testSurface, position, edge);
        }

        private static double?[,] GenerateTestSurface(int rowCount = 200, int columnCount = 300, double k = 0.1)
        {
            double GetValue(int x, int y) => Math.Sin(k * x) * Math.Sin(k * y);

            var grid = new double?[rowCount, columnCount];

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    grid[i, j] = GetValue(i, j);
                }
            }

            return grid;
        }

        private static double GetDelta(IrregularGrid irregularGrid, ValueCalculating calculatingType)
        {
            switch (calculatingType)
            {
                case ValueCalculating.ByRadius:
                    var bounds = irregularGrid.Bounds;
                    return 0.1 * Math.Sqrt(Math.Pow(bounds.XMax - bounds.XMin, 2) + Math.Pow(bounds.YMax - bounds.YMin, 2));
                case ValueCalculating.ByNodesCount:
                    var count = irregularGrid.Nodes.Count();
                    return count > 10 ? count * 0.1 : 10;
                default:
                    throw new ArgumentOutOfRangeException(nameof(calculatingType), calculatingType, null);
            }
        }
    }
}
