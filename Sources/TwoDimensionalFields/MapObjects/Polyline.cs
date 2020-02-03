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

        public List<(double X, double Y)> Nodes { get; } = new List<(double X, double Y)>();

        public void AddNode((double X, double Y) node)
        {
            Nodes.Add(node);
        }

        public void AddNode(double x, double y)
        {
            Nodes.Add((x, y));
        }

        public Bounds CalcBounds()
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

        public void RemoveAllNode()
        {
            Nodes.Clear();
        }

        public void RemoveNode(int index)
        {
            Nodes.RemoveAt(index);
        }

        public void RemoveNode((double X, double Y) item)
        {
            Nodes.Remove(item);
        }

        protected override Bounds GetBounds()
        {
            return bounds = CalcBounds();
        }

        /*internal override bool IsIntersectsWithQuad(Vertex searchPoint, double d)
        {
            if (nodes.Count == 0) return false;
            if (nodes.Count == 1) return IsSegmentIntersectsWithQuad(nodes[0], nodes[0], searchPoint, d);
            for (int x = 0; x < nodes.Count - 1; x++)
                if (IsSegmentIntersectsWithQuad(nodes[x], nodes[x + 1], searchPoint, d))
                    return true;
            return false;
        }*/
    }
}
