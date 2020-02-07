using System;
using TwoDimensionalFields.Maps;

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
    }
}
