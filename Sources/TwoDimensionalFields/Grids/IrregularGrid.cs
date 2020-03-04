using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TwoDimensionalFields.Drawing;
using TwoDimensionalFields.MapObjects;
using TwoDimensionalFields.Maps;

namespace TwoDimensionalFields.Grids
{
    public class IrregularGrid : IGrid, IDrawable, ILayer
    {
        private readonly double maxValue;
        private readonly double minValue;
        private readonly int pow;
        private readonly double searchRadius;
        private readonly double tolerance;

        public IrregularGrid(IEnumerable<Node3d<double>> nodes) : this()
        {
            Nodes = nodes;

            minValue = GetMinValue();
            maxValue = GetMaxValue();

            tolerance = (maxValue - minValue) / Nodes.Count() * 1E-6;

            Bounds = GetBounds();

            searchRadius = Math.Max(Bounds.XMax - Bounds.XMin, Bounds.YMax - Bounds.YMin);

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

        public Bounds Bounds { get; }
        public IrregularGridGraphics GridGraphics { get; }
        public double? MaxValue => maxValue;
        public double? MinValue => minValue;
        public string Name { get; set; }
        public IEnumerable<Node3d<double>> Nodes { get; }
        public bool Selected { get; set; }
        public bool Visible { get; set; }

        public void Draw(IDrawer drawer) => drawer.Draw(this);
        public double? GetValue(double x, double y) => GetValue(x, y, searchRadius, pow);
        public double? GetValue(double x, double y, double searchRadius, int pow) => CalcValueByNodes(x, y, Nodes, 10, pow, tolerance);

        public void SetColors(Dictionary<double, Color> colors) => GridGraphics.Colors = colors;

        private static double? CalcValueByNodes(double x, double y, IEnumerable<Node3d<double>> nodes, double delta, int pow, double tolerance)
        {
            var startTime = DateTimeOffset.Now;

            double Sqr(double value) => value * value;
            double SqrDistance(Node<double> a, Node<double> b) => Sqr(a.X - b.X) + Sqr(a.Y - b.Y);

            var sqrDelta = Sqr(delta);
            var searchNode = new Node<double>(x, y);

            IEnumerable<Node3d<double>> nearestNodes = nodes
                .Where(node => node.X > x - delta && node.X < x + delta)
                .Where(node => node.Y > y - delta && node.Y < y + delta)
                .Where(node => SqrDistance(node, searchNode) <= sqrDelta);

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

            double vSum = 0.0;
            double rSum = 0.0;

            foreach (Node3d<double> node in nearestNodes)
            {
                var distancePow = Math.Pow(SqrDistance(node, searchNode), pow / 2.0);

                vSum += node.Z / distancePow;
                rSum += 1 / distancePow;
            }

            var result = vSum / rSum;
            var calcTime = DateTimeOffset.Now - startTime;
            Console.WriteLine($"Время вычисления {calcTime.Milliseconds} ms.");

            return result;
        }

        private static double? CalcValueByNodes(double x, double y, IEnumerable<Node3d<double>> nodes, int nearestNodesCount, int pow, double tolerance)
        {
            var startTime = DateTimeOffset.Now;

            double Sqr(double value) => value * value;
            double SqrDistance(Node<double> a, Node<double> b) => Sqr(a.X - b.X) + Sqr(a.Y - b.Y);

            var searchNode = new Node<double>(x, y);

            IEnumerable<Node3d<double>> nearestNodes = nodes
                .OrderBy(node => SqrDistance(node, searchNode))
                .Take(nearestNodesCount);

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

            double vSum = 0.0;
            double rSum = 0.0;

            foreach (Node3d<double> node in nearestNodes)
            {
                var distancePow = Math.Pow(SqrDistance(node, searchNode), pow / 2.0);

                vSum += node.Z / distancePow;
                rSum += 1 / distancePow;
            }

            var result = vSum / rSum;
            var calcTime = DateTimeOffset.Now - startTime;
            Console.WriteLine($"Время вычисления {calcTime.Milliseconds} ms.");

            return result;
        }

        private Bounds GetBounds()
        {
            return new Bounds(
                Nodes.Min(node => node.X),
                Nodes.Max(node => node.Y),
                Nodes.Max(node => node.X),
                Nodes.Min(node => node.Y)
            );
        }

        private double GetMaxValue() => Nodes.Max(node => node.Z);
        private double GetMinValue() => Nodes.Min(node => node.Z);
    }
}
