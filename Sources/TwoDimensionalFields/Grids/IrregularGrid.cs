using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TwoDimensionalFields.Drawing.GridGraphics;
using TwoDimensionalFields.MapObjects;
using TwoDimensionalFields.Maps;

namespace TwoDimensionalFields.Grids
{
    public class IrregularGrid : Grid
    {
        private readonly int pow;
        private readonly double searchRadius;
        private readonly double tolerance;

        public IrregularGrid(IEnumerable<Node3d<double>> nodes) : this()
        {
            Nodes = nodes;
            var diagonal = Math.Sqrt(Sqr(Bounds.XMax - Bounds.XMin) + Sqr(Bounds.YMax - Bounds.YMin));
            tolerance = diagonal / Math.Sqrt(Nodes.Count()) * 1E-6;
            searchRadius = diagonal * 0.1;
            pow = 2;

            CalcAndSetParams();
        }

        private IrregularGrid()
        {
            Visible = true;
            Name = "Irregular grid";
            GridGraphics = new IrregularGridGraphics(this);
        }

        public IrregularGridGraphics GridGraphics { get; }
        public IEnumerable<Node3d<double>> Nodes { get; }

        public override double? GetValue(double x, double y) => GetValueByRadius(new Node<double>(x, y), searchRadius, pow);

        public double? GetValueByNodesCount(Node<double> searchPoint, double nearestNodeCount, int pow) =>
            CalcValue(searchPoint, Nodes, nearestNodeCount, GetNearestNodesByCount, pow, tolerance);

        public double? GetValueByRadius(Node<double> searchPoint, double searchRadius, int pow) =>
            CalcValue(searchPoint, Nodes, searchRadius, GetNearestNodesByRadius, pow, tolerance);

        protected override Bounds GetBounds()
        {
            if (Nodes == null || !Nodes.Any())
            {
                return null;
            }

            (double minX, double minY) = Nodes.FirstOrDefault();
            double maxX = minX;
            double maxY = minY;

            foreach ((double x, double y) in Nodes)
            {
                minX = minX < x ? minX : x;
                maxX = maxX > x ? maxX : x;
                minY = minY < y ? minY : y;
                maxY = maxY > y ? maxY : y;
            }

            return new Bounds(minX, maxY, maxX, minY);
        }

        protected override double? GetMaxValue() => Nodes.Any() ? Nodes.Max(node => node.Z) : new double?();
        protected override double? GetMinValue() => Nodes.Any() ? Nodes.Min(node => node.Z) : new double?();

        private static double? CalcValue(
            Node<double> searchPoint,
            IEnumerable<Node3d<double>> nodes,
            double delta,
            Func<IEnumerable<Node3d<double>>, Node<double>, double, IEnumerable<Node3d<double>>> getNearestFunc,
            int pow,
            double tolerance)
        {
            var startTime = DateTimeOffset.Now;

            var nearestNodes = getNearestFunc(nodes, searchPoint, delta);

            if (!nearestNodes.Any())
            {
                return null;
            }

            double vSum = 0.0;
            double rSum = 0.0;

            foreach (Node3d<double> node in nearestNodes)
            {
                var sqrDistance = SqrDistance(node, searchPoint);
                if (Math.Abs(sqrDistance) < tolerance)
                {
                    return node.Z;
                }

                var distancePow = Math.Pow(sqrDistance, pow / 2.0);

                vSum += node.Z / distancePow;
                rSum += 1 / distancePow;
            }

            var result = vSum / rSum;

            var calcTime = DateTimeOffset.Now - startTime;
            Console.WriteLine($"Время вычисления {calcTime.Milliseconds} ms.");

            return result;
        }

        private static IEnumerable<Node3d<double>> GetNearestNodesByCount(IEnumerable<Node3d<double>> nodes, Node<double> searchPoint, double nearestNodesCount)
        {
            var count = Convert.ToInt32(nearestNodesCount);

            return nodes
                .OrderBy(node => SqrDistance(node, searchPoint))
                .Take(count);
        }

        private static IEnumerable<Node3d<double>> GetNearestNodesByRadius(IEnumerable<Node3d<double>> nodes, Node<double> searchPoint, double searchRadius)
        {
            var sqrSearchRadius = Sqr(searchRadius);

            return nodes
                .Where(node => node.X > searchPoint.X - searchRadius && node.X < searchPoint.X + searchRadius)
                .Where(node => node.Y > searchPoint.Y - searchRadius && node.Y < searchPoint.Y + searchRadius)
                .Where(node => SqrDistance(node, searchPoint) <= sqrSearchRadius);
        }

        private static double Sqr(double value) => value * value;
        private static double SqrDistance(Node<double> a, Node<double> b) => Sqr(a.X - b.X) + Sqr(a.Y - b.Y);
        public override void SetColors(Dictionary<double, Color> colors) => GridGraphics.Colors = colors;

        private void CalcAndSetParams()
        {
            if (Nodes == null || !Nodes.Any())
            {
                return;
            }

            (double minX, double minY, double minZ) = Nodes.FirstOrDefault();
            double maxX = minX;
            double maxY = minY;
            double maxZ = minZ;

            foreach ((double x, double y, double z) in Nodes)
            {
                minX = minX < x ? minX : x;
                maxX = maxX > x ? maxX : x;
                minY = minY < y ? minY : y;
                maxY = maxY > y ? maxY : y;
                minZ = minZ < z ? minZ : z;
                maxZ = maxZ > z ? maxZ : z;
            }

            MinValue = minZ;
            MaxValue = maxZ;
            Bounds = new Bounds(minX, maxY, maxX, minY);
        }
    }
}
