using System;
using System.Linq;
using System.Threading;
using TwoDimensionalFields.MapObjects;
using TwoDimensionalFields.Searching;

namespace TwoDimensionalFields.Grids
{
    public class RegularGridFactory
    {
        public static RegularGrid Create(IrregularGrid irregularGrid, int step, ValueCalculating calculatingType, CancellationToken token = default)
        {
            double delta = GetDelta(irregularGrid, calculatingType);
            return Create(irregularGrid, step, delta, 2, calculatingType, token);
        }

        public static RegularGrid Create(IrregularGrid irregularGrid, int step, double delta, int pow, ValueCalculating calculatingType, CancellationToken token = default)
        {
            var position = new Node<double>(irregularGrid.Bounds.XMin, irregularGrid.Bounds.YMax);
            var rowCount = GetRowCount(irregularGrid, step);
            var columnCount = GetColumnCount(irregularGrid, step);

            return Create(irregularGrid, step, position, rowCount, columnCount, delta, pow, calculatingType, token);
        }

        public static RegularGrid Create(IrregularGrid irregularGrid, double step, Node<double> position, int rowCount, int columnCount, double delta, int pow, ValueCalculating calculatingType, CancellationToken token = default)
        {
            var grid = new double?[rowCount, columnCount];
            var squareGrid = new RegularGrid(grid, position, step);

            var getValueFunc = GridValueSearcher.GetSearchingFunc(irregularGrid, calculatingType);

            for (int i = 0; i < rowCount && !token.IsCancellationRequested; i++)
            {
                for (int j = 0; j < columnCount && !token.IsCancellationRequested; j++)
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

        public static int GetColumnCount(IrregularGrid grid, double step) =>
            Convert.ToInt32(Math.Ceiling((grid.Bounds.XMax - grid.Bounds.XMin) / step));

        public static double GetDelta(IrregularGrid irregularGrid, ValueCalculating calculatingType)
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

        public static int GetRowCount(IrregularGrid grid, double step) =>
            Convert.ToInt32(Math.Ceiling((grid.Bounds.YMax - grid.Bounds.YMin) / step));

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
    }
}
