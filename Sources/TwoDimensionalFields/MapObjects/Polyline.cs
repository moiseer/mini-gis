using System;
using System.Collections.Generic;
using TwoDimensionalFields.Maps;

namespace TwoDimensionalFields.MapObjects
{
    public class Polyline : MapObject
    {
        public Polyline()
        {
            objectType = MapObjectType.PolyLine;
        }

        public List<Node<double>> Nodes { get; } = new List<Node<double>>();

        public void AddNode(Node<double> node)
        {
            Nodes.Add(node);
        }

        public void AddNode(double x, double y)
        {
            Nodes.Add(new Node<double>(x, y));
        }

        public void RemoveAllNode()
        {
            Nodes.Clear();
        }

        public void RemoveNode(int index)
        {
            Nodes.RemoveAt(index);
        }

        public void RemoveNode(Node<double> item)
        {
            Nodes.Remove(item);
        }

        protected override Bounds GetBounds()
        {
            return bounds = CalcBounds();
        }

        private Bounds CalcBounds()
        {
            var tempBounds1 = new Bounds();
            var tempBounds2 = new Bounds();
            foreach (var (x, y) in Nodes)
            {
                tempBounds2.SetBounds(x, y, x, y);
                tempBounds1 += tempBounds2;
            }

            return tempBounds1;
        }
    }
}
