using System;

namespace TwoDimensionalFields.MapObjects
{
    public class Polygon : Polyline
    {
        public Polygon()
        {
            objectType = MapObjectType.Polygon;
        }

        public double Area()
        {
            var area = 0.0;
            var n = Nodes.Count;

            for (int i = 0; i < n - 1; ++i)
            {
                area += Nodes[i].X * Nodes[i + 1].Y - Nodes[i + 1].X * Nodes[i].Y;
            }

            area += Nodes[n - 1].X * Nodes[0].Y;
            area -= Nodes[0].X * Nodes[n - 1].Y;

            return Math.Abs(area) / 2.0;
        }

        public bool IsContainPoint(double x, double y)
        {
            var c = false;
            for (int i = 0, j = Nodes.Count - 1; i < Nodes.Count; j = i++)
            {
                if ((Nodes[i].Y <= y && y < Nodes[j].Y || Nodes[j].Y <= y && y < Nodes[i].Y) &&
                    x > (Nodes[j].X - Nodes[i].X) * (y - Nodes[i].Y) / (Nodes[j].Y - Nodes[i].Y) + Nodes[i].X)
                {
                    c = !c;
                }
            }

            return c;
        }

        /*internal override bool IsIntersectsWithQuad(Vertex searchPoint, double d)
        {
            if (base.IsIntersectsWithQuad(searchPoint, d)) return true;

            if (IsSegmentIntersectsWithQuad(Nodes.Last(), Nodes[0], searchPoint, d))
                return true;

            if (IsContainPoint(searchPoint.X, searchPoint.Y)) return true;

            return false;
        }*/
    }
}
