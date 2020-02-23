using System;
using System.Collections.Generic;
using System.Linq;
using TwoDimensionalFields.MapObjects;

namespace TwoDimensionalFields.Grids
{
    public class SquareGridFactory
    {
        public static SquareGrid Create(IEnumerable<Node3d<double>> nodes, int edge)
        {
            double xMin = nodes.Min(node => node.X);
            double xMax = nodes.Max(node => node.X);
            double yMin = nodes.Min(node => node.Y);
            double yMax = nodes.Max(node => node.Y);

            var rowCount = Convert.ToInt32(Math.Ceiling(yMax - yMin) / edge);
            var columnCount = Convert.ToInt32(Math.Ceiling(xMax - xMin) / edge);

            var grid = new double?[rowCount, columnCount];
            var position = new Node<double>(xMin, yMax);
            var squareGrid = new SquareGrid(grid, position, edge);

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    (double x, double y) = squareGrid.IndexesToCoordinates(i, j);
                    double? value = CalcValueByNodes(x, y, nodes, edge * 2);
                    squareGrid.SetValue(i, j, value);
                }
            }

            return new SquareGrid(grid, position, edge);
        }

        public static SquareGrid CreateTestGrid()
        {
            double?[,] testSurface = GenerateTestSurface();
            var position = new Node<double>(0, 0);
            var edge = 2;

            return new SquareGrid(testSurface, position, edge);
        }

        private static double? CalcValueByNodes(double x, double y, IEnumerable<Node3d<double>> nodes, int delta)
        {
            nodes = nodes
                .Where(node => node.X > x - delta && node.X < x + delta)
                .Where(node => node.Y > y - delta && node.Y < y + delta);

            double Distance(Node<double> a, Node<double> b) => Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));

            throw new NotImplementedException();
        }

        private static double?[,] GenerateTestSurface(double k = 0.1)
        {
            int rowCount = 200;
            int columnCount = 300;
            double Z(int x, int y) => Math.Sin(k * x) * Math.Sin(k * y);

            var grid = new double?[rowCount, columnCount];

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    grid[i, j] = Z(i, j);
                }
            }

            return grid;
        }
    }
}
