using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TwoDimensionalFields.Drawing;
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
            tolerance = (MaxValue - MinValue).Value / Nodes.Count() * 1E-6;
            searchRadius = Math.Sqrt(Sqr(Bounds.XMax - Bounds.XMin) + Sqr(Bounds.YMax - Bounds.YMin)) * 0.1;
            pow = 2;
        }

        public IrregularGrid(IEnumerable<Node3d<double>> nodes, double defaultSearchRadius, int defaultPow) : this(nodes)
        {
            searchRadius = defaultSearchRadius;
            pow = defaultPow;
        }

        private IrregularGrid()
        {
            Visible = true;
            Name = "Irregular grid";
            GridGraphics = new IrregularGridGraphics(this);
        }

        public enum ValueCalculating : byte
        {
            ByRadius,
            ByNodesCount
        }

        public IrregularGridGraphics GridGraphics { get; }
        public IEnumerable<Node3d<double>> Nodes { get; }

        public override double? GetValue(double x, double y) => GetValue(new Node<double>(x, y));
        public override double? GetValue(Node<double> searchPoint) => GetValueByRadius(searchPoint, searchRadius, pow);
        public double? GetValueByNodesCount(Node<double> searchPoint, int nearestNodeCount, int pow) => CalcValueByNodesCount(searchPoint, Nodes, nearestNodeCount, pow, tolerance);

        public double? GetValueByRadius(Node<double> searchPoint, double searchRadius, int pow) => CalcValueByRadius(searchPoint, Nodes, searchRadius, pow, tolerance);

        public override void SetColors(Dictionary<double, Color> colors) => GridGraphics.Colors = colors;

        protected override Bounds GetBounds()
        {
            return new Bounds(
                Nodes.Min(node => node.X),
                Nodes.Max(node => node.Y),
                Nodes.Max(node => node.X),
                Nodes.Min(node => node.Y)
            );
        }

        protected override double? GetMaxValue() => Nodes.Max(node => node.Z);
        protected override double? GetMinValue() => Nodes.Min(node => node.Z);

        private static double? CalcValueByNodesCount(Node<double> searchPoint, IEnumerable<Node3d<double>> nodes, int nearestNodesCount, int pow, double tolerance)
        {
            var startTime = DateTimeOffset.Now;

            double Sqr(double value) => value * value;
            double SqrDistance(Node<double> a, Node<double> b) => Sqr(a.X - b.X) + Sqr(a.Y - b.Y);

            IEnumerable<Node3d<double>> nearestNodes = nodes
                .OrderBy(node => SqrDistance(node, searchPoint))
                .Take(nearestNodesCount);

            if (!nearestNodes.Any())
            {
                return null;
            }

            Node3d<double> sameNode = nearestNodes.FirstOrDefault(node =>
                Math.Abs(node.X - searchPoint.X) < tolerance && Math.Abs(node.Y - searchPoint.Y) < tolerance);

            if (sameNode != null)
            {
                return sameNode.Z;
            }

            double vSum = 0.0;
            double rSum = 0.0;

            foreach (Node3d<double> node in nearestNodes)
            {
                var distancePow = Math.Pow(SqrDistance(node, searchPoint), pow / 2.0);

                vSum += node.Z / distancePow;
                rSum += 1 / distancePow;
            }

            var result = vSum / rSum;
            var calcTime = DateTimeOffset.Now - startTime;
            Console.WriteLine($"Время вычисления {calcTime.Milliseconds} ms.");

            return result;
        }

        private static double? CalcValueByRadius(Node<double> searchPoint, IEnumerable<Node3d<double>> nodes, double delta, int pow, double tolerance)
        {
            var startTime = DateTimeOffset.Now;

            var sqrDelta = Sqr(delta);

            IEnumerable<Node3d<double>> nearestNodes = nodes
                .Where(node => node.X > searchPoint.X - delta && node.X < searchPoint.X + delta)
                .Where(node => node.Y > searchPoint.Y - delta && node.Y < searchPoint.Y + delta)
                .Where(node => SqrDistance(node, searchPoint) <= sqrDelta);

            if (!nearestNodes.Any())
            {
                return null;
            }

            Node3d<double> sameNode = nearestNodes.FirstOrDefault(node =>
                Math.Abs(node.X - searchPoint.X) < tolerance && Math.Abs(node.Y - searchPoint.Y) < tolerance);

            if (sameNode != null)
            {
                return sameNode.Z;
            }

            double vSum = 0.0;
            double rSum = 0.0;

            foreach (Node3d<double> node in nearestNodes)
            {
                var distancePow = Math.Pow(SqrDistance(node, searchPoint), pow / 2.0);

                vSum += node.Z / distancePow;
                rSum += 1 / distancePow;
            }

            var result = vSum / rSum;
            var calcTime = DateTimeOffset.Now - startTime;
            Console.WriteLine($"Время вычисления {calcTime.Milliseconds} ms.");

            return result;
        }

        private static double Sqr(double value) => value * value;
        private static double SqrDistance(Node<double> a, Node<double> b) => Sqr(a.X - b.X) + Sqr(a.Y - b.Y);
    }
}
