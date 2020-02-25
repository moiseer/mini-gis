using System;
using System.Collections.Generic;
using System.Linq;
using TwoDimensionalFields.Maps;

namespace TwoDimensionalFields.MapObjects
{
    public class Polyline : MapObject
    {
        public Polyline()
        {
            objectType = MapObjectType.PolyLine;
            Nodes = new List<Node<double>>();
        }

        public List<Node<double>> Nodes { get; }
        public void AddNode(Node<double> node) => Nodes.Add(node);
        public void AddNode(double x, double y) => AddNode(new Node<double>(x, y));
        public void RemoveAllNode() => Nodes.Clear();
        public void RemoveNode(int index) => Nodes.RemoveAt(index);
        public void RemoveNode(Node<double> item) => Nodes.Remove(item);

        protected override Bounds GetBounds()
        {
            return new Bounds(
                Nodes.Min(node => node.X),
                Nodes.Max(node => node.Y),
                Nodes.Max(node => node.X),
                Nodes.Min(node => node.Y)
            );
        }
    }
}
