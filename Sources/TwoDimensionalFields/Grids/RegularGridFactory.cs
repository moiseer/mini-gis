using System;
using System.Collections.Generic;
using System.Linq;
using TwoDimensionalFields.MapObjects;
using TwoDimensionalFields.Searching;

namespace TwoDimensionalFields.Grids
{
    public class RegularGridFactory
    {
        public static RegularGrid Create(IEnumerable<Node3d<double>> nodes, int edge) => Create(nodes, edge, edge * 5, 2);

        public static RegularGrid Create(IEnumerable<Node3d<double>> nodes, int edge, double delta, int pow)
        {
            double xMin = nodes.Min(node => node.X);
            double xMax = nodes.Max(node => node.X);
            double yMin = nodes.Min(node => node.Y);
            double yMax = nodes.Max(node => node.Y);

            var position = new Node<double>(xMin, yMax);
            var rowCount = Convert.ToInt32(Math.Floor(yMax - yMin) / edge) + 1;
            var columnCount = Convert.ToInt32(Math.Floor(xMax - xMin) / edge) + 1;

            return Create(new IrregularGrid(nodes), edge, position, rowCount, columnCount, delta, pow, IrregularGrid.ValueCalculating.ByNodesCount);
        }

        public static RegularGrid Create(IrregularGrid irregularGrid, int edge, Node<double> position, int rowCount, int columnCount, double delta, int pow, IrregularGrid.ValueCalculating calculatingType)
        {
            var grid = new double?[rowCount, columnCount];
            var squareGrid = new RegularGrid(grid, position, edge);

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    var searchPoint = squareGrid.IndexesToCoordinates(i, j);
                    double? value = GridValueSearcher.SearchIrregularGrid(irregularGrid, searchPoint, delta, pow, calculatingType);
                    squareGrid.SetValue(i, j, value);
                }
            }

            return new RegularGrid(grid, position, edge);
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
    }
}
