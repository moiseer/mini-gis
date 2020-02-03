using System;
using TwoDimensionalFields.Maps;

namespace TwoDimensionalFields.MapObjects
{
    /// <summary>
    /// Класс для работы с точечными объектами
    /// </summary>
    public class Point : MapObject
    {
        private readonly (double X, double Y) position;

        public Point(double x, double y) : this()
        {
            position = (x, y);
        }

        public Point((double X, double Y) vertex) : this()
        {
            position = vertex;
        }

        private Point()
        {
            objectType = MapObjectType.Point;
        }

        public double X
        {
            get { return position.X; }
        }

        public double Y
        {
            get { return position.Y; }
        }

        protected override Bounds GetBounds()
        {
            return bounds = new Bounds(position.X, position.Y, position.X, position.Y);
        }

        /*internal override bool IsIntersectsWithQuad(Vertex searchPoint, double d)
        {
            return false;
        }*/
    }
}
