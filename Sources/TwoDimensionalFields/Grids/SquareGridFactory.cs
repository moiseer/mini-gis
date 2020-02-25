using System;
using System.Collections.Generic;
using System.Linq;
using TwoDimensionalFields.MapObjects;

namespace TwoDimensionalFields.Grids
{
    public class SquareGridFactory
    {
        public static SquareGrid Create(IEnumerable<Node3d<double>> nodes, int edge) => Create(nodes, edge, edge * 5, 2);

        public static SquareGrid Create(IEnumerable<Node3d<double>> nodes, int edge, int delta, int pow)
        {
            double xMin = nodes.Min(node => node.X);
            double xMax = nodes.Max(node => node.X);
            double yMin = nodes.Min(node => node.Y);
            double yMax = nodes.Max(node => node.Y);

            var rowCount = Convert.ToInt32(Math.Floor(yMax - yMin) / edge) + 1;
            var columnCount = Convert.ToInt32(Math.Floor(xMax - xMin) / edge) + 1;

            var grid = new double?[rowCount, columnCount];
            var position = new Node<double>(xMin, yMax);
            var squareGrid = new SquareGrid(grid, position, edge);

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    (double x, double y) = squareGrid.IndexesToCoordinates(i, j);
                    double? value = CalcValueByNodes(x, y, nodes, delta, pow);
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

        private static double? CalcValueByNodes(double x, double y, IEnumerable<Node3d<double>> nodes, int delta, int pow)
        {
            var tolerance = 1E-6;

            IEnumerable<Node3d<double>> nearestNodes = nodes
                .Where(node => node.X > x - delta && node.X < x + delta)
                .Where(node => node.Y > y - delta && node.Y < y + delta);

            if (!nearestNodes.Any())
            {
                return null;
            }

            Node3d<double> sameNode = nearestNodes.FirstOrDefault(node =>
                Math.Abs(node.X - x) < tolerance && Math.Abs(node.Y - y) < tolerance);

            if (sameNode != null)
            {
                return sameNode.Z;
            }

            double Distance(Node<double> a, Node<double> b) => Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));

            var searchNode = new Node<double>(x, y);
            double vSum = 0.0;
            double rSum = 0.0;

            foreach (Node3d<double> node in nearestNodes)
            {
                var distancePow = Math.Pow(Distance(searchNode, node), pow);

                vSum += node.Z / distancePow;
                rSum += 1 / distancePow;
            }

            return vSum / rSum;
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
